namespace Orleans.Persistence.Morstead.Etcd.Provider
{
    internal enum MorsteadEtcdProviderErrorCode
    {
        ProvidersBase = 200000,

        // Morstead storage provider related
        MorsteadEtcdProviderBase = ProvidersBase + 2100,
        MorsteadEtcdProvider_DataNotFound = MorsteadEtcdProviderBase + 1,
        MorsteadEtcdProvider_ReadingData = MorsteadEtcdProviderBase + 2,
        MorsteadEtcdProvider_WritingData = MorsteadEtcdProviderBase + 3,
        MorsteadEtcdProvider_Storage_Reading = MorsteadEtcdProviderBase + 4,
        MorsteadEtcdProvider_Storage_Writing = MorsteadEtcdProviderBase + 5,
        MorsteadEtcdProvider_Storage_DataRead = MorsteadEtcdProviderBase + 6,
        MorsteadEtcdProvider_WriteError = MorsteadEtcdProviderBase + 7,
        MorsteadEtcdProvider_DeleteError = MorsteadEtcdProviderBase + 8,
        MorsteadEtcdProvider_InitProvider = MorsteadEtcdProviderBase + 9,
        MorsteadEtcdProvider_ParamConnectionString = MorsteadEtcdProviderBase + 10,
        MorsteadEtcdProvider_DatabaseNotFound = MorsteadEtcdProviderBase +11,
        MorsteadEtcdProvider_ClearingData = 202112,
        MorsteadEtcdProvider_Cleared = 202113,
        MorsteadEtcdProvider_ClearingError = 202114,
        MorsteadEtcdProvider_Storage_Written = 202115,
        MorsteadEtcdProvider_ReadError = 202116
    }
}
