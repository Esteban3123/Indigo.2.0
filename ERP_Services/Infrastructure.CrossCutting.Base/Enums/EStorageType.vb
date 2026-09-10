Imports System.Runtime.Serialization

<DataContract>
Public Enum EStorageType As Integer
    <EnumMember>
    BlobStorage = 1
    <EnumMember>
    LocalStore = 2
End Enum