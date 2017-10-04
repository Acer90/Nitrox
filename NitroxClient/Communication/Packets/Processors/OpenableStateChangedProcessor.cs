﻿using NitroxClient.Communication.Packets.Processors.Abstract;
using NitroxModel.Helper.GameLogic;
using NitroxModel.Helper.Unity;
using NitroxModel.Packets;
using UnityEngine;

namespace NitroxClient.Communication.Packets.Processors
{
    public class OpenableStateChangedProcessor : ClientPacketProcessor<OpenableStateChanged>
    {
        private PacketSender packetSender;

        public OpenableStateChangedProcessor(PacketSender packetSender)
        {
            this.packetSender = packetSender;
        }

        public override void Process(OpenableStateChanged packet)
        {
            GameObject gameObject = GuidHelper.RequireObjectFrom(packet.Guid);            
            Openable openable = gameObject.RequireComponent<Openable>();
            
            using (packetSender.Suppress<OpenableStateChanged>())
            {
                openable.PlayOpenAnimation(packet.IsOpen, packet.Duration);
            }
        }
    }
}
