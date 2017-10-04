using NitroxClient.Communication.Packets.Processors.Abstract;
using NitroxModel.Helper.GameLogic;
using NitroxModel.Helper.Unity;
using NitroxModel.Packets;
using UnityEngine;

namespace NitroxClient.Communication.Packets.Processors
{
    public class CyclopsBeginSilentRunningProcessor : ClientPacketProcessor<CyclopsBeginSilentRunning>
    {
        private PacketSender packetSender;

        public CyclopsBeginSilentRunningProcessor(PacketSender packetSender)
        {
            this.packetSender = packetSender;
        }

        public override void Process(CyclopsBeginSilentRunning packet)
        {
            GameObject cyclops = GuidHelper.RequireObjectFrom(packet.Guid);
            CyclopsSilentRunningAbilityButton ability = cyclops.RequireComponentInChildren<CyclopsSilentRunningAbilityButton>();
            
            using (packetSender.Suppress<CyclopsBeginSilentRunning>())
            {
                ability.subRoot.BroadcastMessage("RigForSilentRunning");
                ability.InvokeRepeating("SilentRunningIteration", 0f, ability.silentRunningIteration);
            }
        }
    }
}
