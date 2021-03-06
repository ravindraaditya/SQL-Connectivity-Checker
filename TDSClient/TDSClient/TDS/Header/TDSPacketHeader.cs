﻿//  ---------------------------------------------------------------------------
//  <copyright file="TDSPacketHeader.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
//  </copyright>
//  ---------------------------------------------------------------------------

namespace TDSClient.TDS.Header
{
    using System;
    using System.IO;
    using TDSClient.TDS.Interfaces;
    using TDSClient.TDS.Utilities;

    public class TDSPacketHeader : IPackageable
    {
        public TDSMessageType Type { get; set; }

        public TDSMessageStatus Status { get; set; }

        public ushort Length { get; set; }

        public ushort SPID { get; set; }

        public byte Packet { get; set; }

        public byte Window { get; set; }

        public int ConvertedPacketLength
        {
            get
            {
                return Convert.ToInt32(Length);
            }
        }

        public TDSPacketHeader()
        {
        }

        public TDSPacketHeader(TDSMessageType type, TDSMessageStatus status, ushort spid = 0x0000, byte packet = 0x00, byte window = 0x00)
        {
            Type = type;
            Status = status;
            SPID = spid;
            Packet = packet;
            Window = window;
        }

        public void Pack(MemoryStream stream)
        {
            stream.WriteByte((byte)Type);
            stream.WriteByte((byte)Status);
            BigEndianUtilities.WriteUShort(stream, Length);
            BigEndianUtilities.WriteUShort(stream, SPID);
            stream.WriteByte(Packet);
            stream.WriteByte(Window);
        }

        public bool Unpack(MemoryStream stream)
        {
            Type = (TDSMessageType)stream.ReadByte();
            Status = (TDSMessageStatus)stream.ReadByte();
            Length = BigEndianUtilities.ReadUShort(stream);
            SPID = BigEndianUtilities.ReadUShort(stream);
            Packet = Convert.ToByte(stream.ReadByte());
            Window = Convert.ToByte(stream.ReadByte());
            return true;
        }
    }
}
