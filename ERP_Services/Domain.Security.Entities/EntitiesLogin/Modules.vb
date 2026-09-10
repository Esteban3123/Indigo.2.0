Imports System.Runtime.Serialization
Imports Domain.Base.Entities

<DataContract(IsReference:=True)>
Public Class Modules
    Implements ICloneable

    Sub New()
        Me.ListForm = New List(Of VieDBForm)
    End Sub

    <DataMember()>
    Property IdModule As Integer

    <DataMember()>
    Property ModuleName As String

    <DataMember()>
    Property Description As String

    <DataMember()>
    Property State As Byte

    <DataMember()>
    Property ListForm As List(Of VieDBForm)

    <DataMember()>
    Property ProductCatalog As ProductCatalog

    <DataMember()>
    Property ModuleTitle As List(Of ModuleTitle)

    <DataMember()>
    Property ModuleForm As List(Of ModuleForm)

    <DataMember()>
    Property Crud As ECrud

    ''' <summary>
    ''' Clona el objeto para que obtenga una instancia distinta al actual
    ''' </summary>
    ''' <returns></returns>
    Public Function Clone() As Object Implements ICloneable.Clone
        Dim _Clone As Modules = Nothing
        Dim obj = New System.Runtime.Serialization.DataContractSerializer(GetType(Modules))

        Using stream = New System.IO.MemoryStream()
            obj.WriteObject(stream, Me)
            stream.Seek(0, System.IO.SeekOrigin.Begin)
            _Clone = CType(obj.ReadObject(stream), Modules)
        End Using

        Return _Clone
    End Function
End Class
