Imports System.Runtime.Serialization

<DataContract(IsReference:=True), Serializable(), KnownType(GetType(IngresoOrdenesServicio))> _
Partial Public Class IngresoOrdenesServicio

    <DataMember()> _
    Public Property NUMINGRES As String
    <DataMember()> _
    Public Property IPCODPACI As String
    <DataMember()> _
    Public Property TIPOINGRE As Integer
    <DataMember()> _
    Public Property IINGREPOR As Integer
    <DataMember()> _
    Public Property CODENTIDA As String
    <DataMember()> _
    Public Property ITIPORIES As Integer
    <DataMember()> _
    Public Property ICAUSAING As Integer
    <DataMember()> _
    Public Property IFECHAING As Date
    <DataMember()> _
    Public Property ILIQUIDAC As Integer
    <DataMember()> _
    Public Property ICONTROLI As String
    <DataMember()> _
    Public Property CODCENATE As String
    <DataMember()> _
    Public Property UFUCODIGO As String
    <DataMember()> _
    Public Property IAUTORIZA As String
    <DataMember()> _
    Public Property IESTADOIN As String
    <DataMember()> _
    Public Property IINGRESOA As String
    <DataMember()> _
    Public Property ISOATVALO As Nullable(Of Decimal)
    <DataMember()> _
    Public Property ISALCODIG As String
    <DataMember()> _
    Public Property INUMERORE As String
    <DataMember()> _
    Public Property IFECHAREM As Nullable(Of Date)
    <DataMember()> _
    Public Property IAUTORREM As String
    <DataMember()> _
    Public Property DEPMUNCOD As String
    <DataMember()> _
    Public Property AIPSREMIS As String
    <DataMember()> _
    Public Property IOBSERVAC As String
    <DataMember()> _
    Public Property IJUSTIFIC As String
    <DataMember()> _
    Public Property IREINGRES As Integer
    <DataMember()> _
    Public Property UFUINGMED As String
    <DataMember()> _
    Public Property CODPROING As String
    <DataMember()> _
    Public Property UFUEGRMED As String
    <DataMember()> _
    Public Property CODPROEGR As String
    <DataMember()> _
    Public Property UFUINGHOS As String
    <DataMember()> _
    Public Property UFUEGRHOS As String
    <DataMember()> _
    Public Property CODESPTRA As String
    <DataMember()> _
    Public Property TIPOPROFE As String
    <DataMember()> _
    Public Property UFUAACTMED As String
    <DataMember()> _
    Public Property UFUAACTHOS As String
    <DataMember()> _
    Public Property CODCAMACT As Nullable(Of Integer)
    <DataMember()> _
    Public Property CODDIAING As String
    <DataMember()> _
    Public Property CODDIAEGR As String
    <DataMember()> _
    Public Property UFUACTPAC As String
    <DataMember()> _
    Public Property CODUSUCRE As String
    <DataMember()> _
    Public Property FECREGCRE As Nullable(Of Date)
    <DataMember()> _
    Public Property CODUSUMOD As String
    <DataMember()> _
    Public Property FECREGMOD As Nullable(Of Date)
    <DataMember()> _
    Public Property CODUSUANU As String
    <DataMember()> _
    Public Property FECREGANU As Nullable(Of Date)
    <DataMember()> _
    Public Property NUMINGREI As String
    <DataMember()> _
    Public Property CODICAMHO As String
    <DataMember()> _
    Public Property FECHOSPIT As Nullable(Of Date)
    <DataMember()> _
    Public Property INDAUDFOR As Decimal
    <DataMember()> _
    Public Property IPRNOMBRE As String
    <DataMember()> _
    Public Property IPCODACTR As String
    <DataMember()> _
    Public Property IPEXPEDIC As String
    <DataMember()> _
    Public Property FECACTRAN As Nullable(Of Date)
    <DataMember()> _
    Public Property HORACIDEN As String
    <DataMember()> _
    Public Property IPTELEFON As String
    <DataMember()> _
    Public Property OBSERACIT As String
    <DataMember()> _
    Public Property OBSERAREM As String
    <DataMember()> _
    Public Property INGRECEXT As Nullable(Of Boolean)
    <DataMember()> _
    Public Property PACATENDI As Nullable(Of Boolean)
    <DataMember()> _
    Public Property ESCADOWNT As String
    <DataMember()> _
    Public Property ESCABIERI As String
    <DataMember()> _
    Public Property ESCARASS As String
    <DataMember()> _
    Public Property ESCNORPAC As String
    <DataMember()> _
    Public Property SERSUSCEP As Nullable(Of Boolean)
    <DataMember()> _
    Public Property ESCVASPAC As String
    <DataMember()> _
    Public Property ESCAPAPAC As String
    <DataMember()> _
    Public Property VIVESOLO As Nullable(Of Boolean)
    <DataMember()> _
    Public Property GENCAREGROUP As Nullable(Of Integer)
    <DataMember()> _
    Public Property GENULTLIQUI As Nullable(Of Date)
    <DataMember()> _
    Public Property GENCONENTITY As Nullable(Of Integer)
    <DataMember()> _
    Public Property NUMTRIAGEI As String
    <DataMember()>
    Public Property SOLRESHEMO As Boolean
    <DataMember()>
    Public Property TRATAESPECIA As Integer
    <DataMember()>
    Public Property IDUBICACION As Nullable(Of Integer)

    <DataMember()>
    Public Property IdAdmissionType As Integer?

    ''' <summary>
    ''' Id de Vías Ingreso Servicios de Salud asociado  al ingreso
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property IdEntryRoutesHealthServices As Integer?

    ''' <summary>
    ''' Id Finalidades tecnologias de la salud asociado  al ingreso
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property IdHealthPurposes As Integer?

    ''' <summary>
    ''' Id Modalidades de Atención asociado al ingreso
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property IdAdmissionModalities As Integer?

End Class
