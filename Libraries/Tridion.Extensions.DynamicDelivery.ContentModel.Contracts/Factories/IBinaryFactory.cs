﻿using System;
namespace Tridion.Extensions.DynamicDelivery.ContentModel.Factories
{
    public interface IBinaryFactory
    {
        bool TryFindBinary(string url, out IBinary binary);
        IBinary FindBinary(string url);
        bool TryGetBinary(string tcmUri, out IBinary binary);       
        IBinary GetBinary(string tcmUri);
        bool TryFindBinaryContent(string url, out byte[] bytes);
        byte[] FindBinaryContent(string url);
        bool TryGetBinaryContent(string tcmUri, out byte[] bytes);
        byte[] GetBinaryContent(string tcmUri);
        bool HasBinaryChanged(string url);
    }
}
