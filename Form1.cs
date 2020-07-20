using ENet.Managed;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace ACCOUNTCHECK
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        ENetHost eNet;
        ENetPeer eNetP;
        public int Growtopia_Port = 17279; // todo auto get port
        public string Growtopia_IP = "213.179.209.168";
        public string Growtopia_Master_IP = "213.179.209.168";
        public int Growtopia_Master_Port = 17279;
        public static string doorid = "";
        public static string tankIDName = "";
        public static string tankIDPass = "";
        public static string game_version = "3.37";
        public static string country = "us";
        public static string macc = "02:15:01:20:30:05";
        public static int token = 0;
        public static bool resetStuffNextLogon = false;
        public static int userID = 0;
        public static int lmode = 0;
        public class PacketSending
        {
            private Random rand = new Random();
            public void SendData(byte[] data, ENetPeer peer, ENetPacketFlags flag = ENetPacketFlags.Reliable)
            {

                if (peer == null) return;
                if (peer.State != ENetPeerState.Connected) return;

                if (rand.Next(0, 1) == 0) peer.Send(data, 0, flag);
                else peer.Send(data, 1, flag);
            }

            public void SendPacketRaw(int type, byte[] data, ENetPeer peer, ENetPacketFlags flag = ENetPacketFlags.Reliable)
            {
                byte[] packetData = new byte[data.Length + 5];
                Array.Copy(BitConverter.GetBytes(type), packetData, 4);
                Array.Copy(data, 0, packetData, 4, data.Length);
                SendData(packetData, peer);
            }

            public void SendPacket(int type, string str, ENetPeer peer, ENetPacketFlags flag = ENetPacketFlags.Reliable)
            {
                SendPacketRaw(type, Encoding.ASCII.GetBytes(str.ToCharArray()), peer);
            }

            public void SecondaryLogonAccepted(ENetPeer peer)
            {
                SendPacket((int)NetTypes.NetMessages.GENERIC_TEXT, string.Empty, peer);
            }

            public void InitialLogonAccepted(ENetPeer peer)
            {
                SendPacket((int)NetTypes.NetMessages.SERVER_HELLO, string.Empty, peer);
            }
        }
        public class NetTypes
        {
            public enum PacketTypes
            {
                PLAYER_LOGIC_UPDATE = 0,
                CALL_FUNCTION,
                UPDATE_STATUS,
                TILE_CHANGE_REQ,
                LOAD_MAP,
                TILE_EXTRA,
                TILE_EXTRA_MULTI,
                TILE_ACTIVATE,
                APPLY_DMG,
                INVENTORY_STATE,
                ITEM_ACTIVATE,
                ITEM_ACTIVATE_OBJ,
                UPDATE_TREE,
                MODIFY_INVENTORY_ITEM,
                MODIFY_ITEM_OBJ,
                APPLY_LOCK,
                UPDATE_ITEMS_DATA,
                PARTICLE_EFF,
                ICON_STATE,
                ITEM_EFF,
                SET_CHARACTER_STATE,
                PING_REPLY,
                PING_REQ,
                PLAYER_HIT,
                APP_CHECK_RESPONSE,
                APP_INTEGRITY_FAIL,
                DISCONNECT,
                BATTLE_JOIN,
                BATTLE_EVENT,
                USE_DOOR,
                PARENTAL_MSG,
                GONE_FISHIN,
                STEAM,
                PET_BATTLE,
                NPC,
                SPECIAL,
                PARTICLE_EFFECT_V2,
                ARROW_TO_ITEM,
                TILE_INDEX_SELECTION,
                UPDATE_PLAYER_TRIBUTE
            };

            public enum NetMessages
            {
                UNKNOWN = 0,
                SERVER_HELLO,
                GENERIC_TEXT,
                GAME_MESSAGE,
                GAME_PACKET,
                ERROR,
                TRACK,
                LOG_REQ,
                LOG_RES
            };

        }
        class VariantList
        {
            // this class has been entirely made by me, based on the code available on the gt bot of anybody :)
            [DllImport("msvcrt.dll", EntryPoint = "memcpy", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
            public static extern IntPtr memcpy(IntPtr dest, IntPtr src, UIntPtr count);

            public struct VarList
            {
                public string FunctionName;
                public int netID;
                public uint delay;
                public object[] functionArgs;
            };

            public enum OnSendToServerArgs
            {
                port = 1,
                token,
                userId,
                IPWithExtraData = 4
            };

            public byte[] get_extended_data(byte[] pktData)
            {
                return pktData.Skip(56).ToArray();
            }

            public byte[] get_struct_data(byte[] package)
            {
                int packetLen = package.Length;
                if (packetLen >= 0x3c)
                {
                    byte[] structPackage = new byte[packetLen - 4];
                    Array.Copy(package, 4, structPackage, 0, packetLen - 4);
                    int p2Len = BitConverter.ToInt32(package, 56);
                    if (((byte)(package[16]) & 8) != 0)
                    {
                    }
                    else
                    {
                        Array.Copy(BitConverter.GetBytes(0), 0, package, 56, 4);
                    }
                    return structPackage;
                }
                return null;
            }

            public VarList GetCall(byte[] package)
            {

                VarList varList = new VarList();
                //if (package.Length < 60) return varList;
                int pos = 0;
                //varList.netID = BitConverter.ToInt32(package, 8);
                //varList.delay = BitConverter.ToUInt32(package, 24);
                byte argsTotal = package[pos];
                pos++;
                if (argsTotal > 7) return varList;
                varList.functionArgs = new object[argsTotal];

                for (int i = 0; i < argsTotal; i++)
                {
                    varList.functionArgs[i] = 0; // just to be sure...
                    byte index = package[pos]; pos++; // pls dont bully sm
                    byte type = package[pos]; pos++;


                    switch (type)
                    {
                        case 1:
                            {
                                float vFloat = BitConverter.ToUInt32(package, pos); pos += 4;
                                varList.functionArgs[index] = vFloat;
                                break;
                            }
                        case 2: // string
                            int strLen = BitConverter.ToInt32(package, pos); pos += 4;
                            string v = string.Empty;
                            v = Encoding.ASCII.GetString(package, pos, strLen); pos += strLen;

                            if (index == 0)
                                varList.FunctionName = v;

                            if (index > 0)
                            {
                                if (varList.FunctionName == "OnSendToServer") // exceptionary function, having it easier like this :)
                                {
                                    doorid = v.Substring(v.IndexOf("|") + 1); // doorid
                                    if (v.Length >= 8)
                                        v = v.Substring(0, v.IndexOf("|"));
                                }

                                varList.functionArgs[index] = v;
                            }
                            break;
                        case 5: // uint
                            uint vUInt = BitConverter.ToUInt32(package, pos); pos += 4;
                            varList.functionArgs[index] = vUInt;
                            break;
                        case 9: // int (can hold negative values, of course they are always casted but its just a sign from the server that the value was intended to hold negative values as well)
                            int vInt = BitConverter.ToInt32(package, pos); pos += 4;
                            varList.functionArgs[index] = vInt;
                            break;
                        default:
                            break;
                    }
                }
                return varList;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
        }
        static string yeterfuck;
        static string nopefuck;
        private void Peer_OnDisconnect_Client(object sender, uint e)
        {
            try
            {
                yeterfuck = File.ReadAllText("s.temp");
                nopefuck = File.ReadAllText("d.temp");
                ListViewItem dsadw = new ListViewItem(animaTextBox4.Text);
                dsadw.SubItems.Add(animaTextBox3.Text);
                string[] somearrays = yeterfuck.Split('\n');
                string[] somearraysss = nopefuck.Split('\n');
                try
                {
                    for (int i = 0; i < somearrays.Length; i++)
                    {
                        if(somearrays[i].Contains("Worldlock"))
                        {
                            dsadw.SubItems.Add(somearrays[i].ToLower().Replace("worldlock_balance|", ""));
                        }
                    }
                    for (int ix = 0; ix < somearraysss.Length; ix++)
                    {
                        if (somearraysss[ix].Contains("Gems"))
                        {
                            dsadw.SubItems.Add(somearraysss[ix].ToLower().Replace("gems_balance|",""));
                        }
                    }
                    updatestatus("Checked Success!", 2);
                    animaExperimentalListView2.Add(dsadw);
                    File.Delete("d.temp");
                    File.Delete("s.temp");
                    animaTextBox4.Enabled = true;
                    animaTextBox3.Enabled = true;
                    animaButton6.Enabled = true;
                    animaButton5.Enabled = true;
                }
                catch
                {

                }

            }
            catch
            {

            }
        }
        public void ConnectCurrent()
        {
            if (eNet == null) return;

            if (eNet.ServiceThreadStarted)
            {

                if (eNetP == null)
                {
                    eNetP = eNet.Connect(new System.Net.IPEndPoint(IPAddress.Parse(Growtopia_IP), Growtopia_Port), 2, 0);
                }
                else if (eNetP.State == ENetPeerState.Connected)
                {
                    eNetP.Reset();

                    eNetP = eNet.Connect(new System.Net.IPEndPoint(IPAddress.Parse(Growtopia_IP), Growtopia_Port), 2, 0);
                }
            }
        }
        public static string CreateLogonPacket(string customGrowID = "", string customPass = "")
        {
            string p = string.Empty;
            Random rand = new Random();
            bool requireAdditionalData = false; if (token > 0 || token < 0) requireAdditionalData = true;

            if (customGrowID == "")
            {
                if (tankIDName != "")
                {
                    p += "tankIDName|" + (tankIDName + "\n");
                    p += "tankIDPass|" + (tankIDPass + "\n");
                }
            }
            else
            {
                p += "tankIDName|" + (customGrowID + "\n");
                p += "tankIDPass|" + (customPass + "\n");
            }

            p += "requestedName|" + ("Growbrew" + rand.Next(0, 255).ToString() + "\n"); //"Growbrew" + rand.Next(0, 255).ToString() + "\n"
            p += "f|1\n";
            p += "protocol|94\n";
            p += "game_version|" + (game_version + "\n");
            if (requireAdditionalData) p += "lmode|" + lmode + "\n";
            p += "cbits|0\n";
            p += "player_age|100\n";
            p += "GDPR|1\n";
            p += "hash2|" + rand.Next(-777777777, 777777777).ToString() + "\n";
            p += "meta|localhost\n"; // soon auto fetch meta etc.
            p += "fhash|-716928004\n";
            p += "platformID|4\n";
            p += "deviceVersion|0\n";
            p += "country|" + (country + "\n");
            p += "hash|" + rand.Next(-777777777, 777777777).ToString() + "\n";
            p += "mac|" + macc + "\n";
            if (requireAdditionalData) p += "user|" + (userID.ToString() + "\n");
            if (requireAdditionalData) p += "token|" + (token.ToString() + "\n");
            if (doorid != "") p += "doorID|" + doorid.ToString() + "\n";
            p += "wk|" + ("NONE0\n");
            //p += "zf|-1576181843";
            return p;
        }
        void updatestatus(string s,int type)
        {
            if(type ==1)
            {
                animaStatusBar1.Type = AnimaStatusBar.Types.Basic;
                animaStatusBar1.Text = s;
            }
            else if(type ==2)
            {
                animaStatusBar1.Type = AnimaStatusBar.Types.Success;
                animaStatusBar1.Text = s;
            }
            else if (type == 3)
            {
                animaStatusBar1.Type = AnimaStatusBar.Types.Wrong;
                animaStatusBar1.Text = s;
            }
        }
        private void Peer_OnReceive_Client(object sender, ENetPacket e)
        {
            try
            {
                // this is a specific, external client made only for the purpose of using the TRACK packet for our gains/advantage in order to check all accounts quick and efficiently.
                byte[] packet = e.GetPayloadFinal();
                Console.WriteLine("RECEIVE TYPE: " + packet[0].ToString());
                updatestatus("Received:"+packet[0].ToString(),1);
                switch (packet[0])
                {
                    case 1: // HELLO server packet.
                        {
                            PacketSending packetSender = new PacketSending();
                            packetSender.SendPacket(2, CreateLogonPacket(animaTextBox4.Text, animaTextBox3.Text), eNetP);
                            updatestatus("Loginning: " + packet[0].ToString(), 1);
                            break;
                        }
                    case 2:
                    case 3:
                        {
                            Console.WriteLine("[ACCOUNT-CHECKER] TEXT PACKET CONTENT:\n" + Encoding.ASCII.GetString(packet.Skip(4).ToArray()));
                            string game = Encoding.ASCII.GetString(packet.Skip(4).ToArray());
                            if (game.Contains("suspend"))
                            {
                                updatestatus("Account Suspended!", 3);
                                eNetP.Disconnect(0);
                            }
                            if (game.Contains("ban"))
                            {
                                updatestatus("Account Banned!", 3);
                                eNetP.Disconnect(0);
                            }
                            if (game.Contains("maint"))
                            {
                                updatestatus("Growtopia servers fuck!", 3);
                                eNetP.Disconnect(0);
                            }
                            if (game.Contains("play.sfx"))
                            {
                                updatestatus("Account Bug!", 3);
                                eNetP.Disconnect(0);
                            }
                            if (game.Contains("UPDATE REQUIRED"))
                            {
                                game.Replace("msg|`4", "");
                                game = Regex.Match(game, @"\d+").Value;
                                game = game.Insert(1,".");
                                ACCOUNTCHECK.Properties.Settings.Default.gamever = game;
                                ACCOUNTCHECK.Properties.Settings.Default.Save();
                                ACCOUNTCHECK.Properties.Settings.Default.gamever = game_version;
                                Console.WriteLine("fuckchecker:" + game);
                                updatestatus("Restart need!", 1);
                            }
                            if(game.Contains("password is wrong"))
                            {
                                updatestatus("Wrong Password!", 3);
                                eNetP.Disconnect(0);
                            }//Incorrect logon token..
                            if (game.Contains("Incorrect logon token"))
                            {
                                VariantList ad = new VariantList();
                                byte[] tankPacket = ad.get_struct_data(packet);
                                VariantList.VarList vList = ad.GetCall(ad.get_extended_data(tankPacket));
                                vList.netID = BitConverter.ToInt32(tankPacket, 4); // add netid
                                vList.delay = BitConverter.ToUInt32(tankPacket, 20); // add keep track of delay modifier
                                string ip = (string)vList.functionArgs[4];

                                if (ip.Contains("|"))
                                    ip = ip.Substring(0, ip.IndexOf("|"));

                                int port = (int)vList.functionArgs[1];
                                userID = (int)vList.functionArgs[3];
                                token = (int)vList.functionArgs[2];
                                lmode = (int)vList.functionArgs[5];
                                Growtopia_IP = ip;
                                Growtopia_Port = port;
                                ConnectCurrent();
                                updatestatus("Peer Reset Success!", 1);
                                Thread.Sleep(10);
                            }//Incorrect logon token..
                            break;
                        }
                    case 4:
                        {
                            VariantList ad = new VariantList();
                            byte[] tankPacket = ad.get_struct_data(packet);
                            if (tankPacket[0] == 1)
                            {
                                VariantList.VarList vList = ad.GetCall(ad.get_extended_data(tankPacket));
                                vList.netID = BitConverter.ToInt32(tankPacket, 4); // add netid
                                vList.delay = BitConverter.ToUInt32(tankPacket, 20); // add keep track of delay modifier

                                // Console.WriteLine(VarListFetched.FunctionName);
                                if (vList.FunctionName == "OnSendToServer")
                                {
                                    string ip = (string)vList.functionArgs[4];

                                    if (ip.Contains("|"))
                                        ip = ip.Substring(0, ip.IndexOf("|"));

                                    int port = (int)vList.functionArgs[1];
                                     userID = (int)vList.functionArgs[3];
                                     token = (int)vList.functionArgs[2];
                                     lmode = (int)vList.functionArgs[5];
                                    Growtopia_IP = ip;
                                    Growtopia_Port = port;
                                    ConnectCurrent();
                                    updatestatus("Peer Reset Success!", 1);
                                }
                                // variant call, just rn used for subserver switching
                            }
                            break;
                        }

                    case (byte)NetTypes.NetMessages.TRACK: // TRACK packet.
                        {
                            Console.WriteLine("[ACCOUNT-CHECKER] TRACK PACKET CONTENT:\n" + Encoding.ASCII.GetString(packet.Skip(4).ToArray()));
                            File.AppendAllText("s.temp", Encoding.ASCII.GetString(packet.Skip(4).ToArray()));
                            Growtopia_Port = Growtopia_Master_Port; // todo auto get port
                            Growtopia_IP = Growtopia_Master_IP;
                            PacketSending asd = new PacketSending();
                            asd.SendPacket(2,"action|enter_game",eNetP);

                            if (Encoding.ASCII.GetString(packet.Skip(4).ToArray()).Contains("Gem"))
                            {
                                File.AppendAllText("d.temp",Encoding.ASCII.GetString(packet.Skip(4).ToArray()));
                                eNetP.Disconnect(0);
                            }
                            break;
                        }
                    default:
                        break;
                }
            }
            catch
            {

            }
        }
        private void Client_OnConnect(object sender, ENetConnectEventArgs e)
        {
            e.Peer.OnReceive += Peer_OnReceive_Client;
            e.Peer.OnDisconnect += Peer_OnDisconnect_Client;
            e.Peer.PingInterval(875);
            e.Peer.Timeout(1000, 7000, 15000);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            game_version = ACCOUNTCHECK.Properties.Settings.Default.gamever;
        }

        private void animaButton1_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }

        private void animaButton2_Click(object sender, EventArgs e)
        {
            eNet = new ENetHost(1, 2);
            eNet.OnConnect += Client_OnConnect;
            eNet.CompressWithRangeCoder();
            eNet.ChecksumWithCRC32();
            eNet.StartServiceThread();
            eNetP = eNet.Connect(new System.Net.IPEndPoint(IPAddress.Parse(Growtopia_Master_IP), Growtopia_Master_Port), 2, 0);
        }

        private void animaButton6_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void animaForm2_Click(object sender, EventArgs e)
        {

        }

        private void animaButton6_Click_1(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void animaButton4_Click(object sender, EventArgs e)
        {
            updatestatus("Used Growbrewproxy account checker in this project. project by arhtax#2020", 1);
        }

        private void animaButton5_Click(object sender, EventArgs e)
        {
            animaTextBox4.Enabled = false;
            animaTextBox3.Enabled = false;
            animaButton6.Enabled = false;
            animaButton5.Enabled = false;
            ManagedENet.Startup();
            eNet = new ENetHost(1, 2);
            eNet.OnConnect += Client_OnConnect;
            eNet.CompressWithRangeCoder();
            eNet.ChecksumWithCRC32();
            eNet.StartServiceThread();
            eNetP = eNet.Connect(new System.Net.IPEndPoint(IPAddress.Parse(Growtopia_Master_IP), Growtopia_Master_Port), 2, 0);
            updatestatus("Connected!",2);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start("https://discord.gg/psv3Yb");
        }

        private void animaForm1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)

            {

                this.Close();

            }
        }

        private void animaForm1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void animaForm1_Click(object sender, EventArgs e)
        {
            this.KeyPreview = true;
        }
    }
}
