Imports System.Runtime.Serialization
''' <summary>
''' Enumeracion que determina si la compañia es del sector Privado o Publico
''' </summary>
''' <remarks></remarks>
<DataContract()>
Public Enum eCompanyType As Integer
    ''' <summary>
    ''' representa el sector privado
    ''' </summary>
    ''' <remarks></remarks>
    <EnumMember()>
    PrivateCompany = 1
    ''' <summary>
    ''' representa el sector publico
    ''' </summary>
    ''' <remarks></remarks>
    <EnumMember()>
    PublicCompany = 2
End Enum