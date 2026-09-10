Imports System.Runtime.Serialization
''' <summary>
''' Enumeracion que determina si la compañia es del sector Privado o Publico
''' </summary>
''' <remarks></remarks>
<DataContract()>
Public Enum eMasterAccount As Integer
    ''' <summary>
    ''' folio que No es cuenta madre ni pertenece a sus derivaciones
    ''' </summary>
    <EnumMember()>
    NotMasterAccount = 0

    ''' <summary>
    ''' Folio cuenta madre el cual, se separá en folio entidad y paciente
    ''' </summary>
    <EnumMember()>
    MasterAccount = 1

    ''' <summary>
    ''' Folio de la aseguradora
    ''' </summary>
    <EnumMember()>
    EntityAccount = 2

    ''' <summary>
    ''' folio paciente o tercero
    ''' </summary>
    <EnumMember()>
    PatientAccount = 4

    ''' <summary>
    ''' Folio cuenta madre Original Oculto( ya separado)
    ''' </summary>
    <EnumMember()>
    HideMasterAccount = 3
End Enum