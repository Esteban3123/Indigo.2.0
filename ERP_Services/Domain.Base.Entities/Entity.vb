
#Region "Imports"

Imports System.Runtime.Serialization
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Reflection
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations.Schema

#End Region

''' <summary>
''' Clase base que representa una entidad
''' </summary>
''' <typeparam name="TEntity">Tipo de la entidad a representar</typeparam>
<DataContract(IsReference:=True), Serializable()>
Public MustInherit Class Entity(Of TEntity As IObjectWithChangeTracker)

#Region "Original Value"

    ''' <summary>
    ''' Obtiene o asigna una copia con los valores originales de la entidad
    ''' </summary>
    ''' <value>Copia de la entidad</value>
    ''' <returns>Una copia de la entidad con los valores originales</returns>
    <DataMember(), NotMapped()>
    Public Property OriginalValue As TEntity

    ''' <summary>
    ''' Obtiene o asigna el conjunto de campos personalizados
    ''' </summary>
    ''' <value>Conjunto de campos personalizados</value>
    ''' <returns>El conjunto de campos personalizados</returns>
    <DataMember(), NotMapped()>
    Public Property CustomProperties As DataSet
#End Region

#Region "Methods"

    ''' <summary>
    ''' Crea una copia de la entidad
    ''' </summary>
    ''' <returns>Copia de la entidad</returns>
    Public Function Clone() As TEntity
        Dim ms As New MemoryStream()
        Dim bf As New BinaryFormatter()

        bf.Serialize(ms, Me)

        ms.Position = 0
        Dim obj As Object = bf.Deserialize(ms)
        ms.Close()

        Return CTypeDynamic(obj, GetType(TEntity))
    End Function

    Public Function AsXml(Optional entity As Object = Nothing) As String
        Dim formatXml As String = "<{0}>{1}</{0}>"
        If entity Is Nothing Then entity = Me
        Dim builder As New Text.StringBuilder()
        builder.Append("<" & entity.GetType().Name & ">")
        'Propuiedades simples
        For Each entityProperty As System.Reflection.PropertyInfo In entity.GetType().GetProperties().Where(Function(o) Not o.Name.Equals("TimeStamp") AndAlso o.PropertyType.Namespace.Equals("System"))
            Dim type As Type = If(Nullable.GetUnderlyingType(entityProperty.PropertyType), entityProperty.PropertyType)
            If entityProperty.GetValue(entity) IsNot Nothing Then
                If type.Name.Equals("DateTime") Then
                    builder.Append(String.Format(formatXml, entityProperty.Name, CType(entityProperty.GetValue(entity), DateTime).ToString("dd/MM/yyyy HH:mm:ss")))
                ElseIf type.Name.Equals("Decimal") Then
                    builder.Append(String.Format(formatXml, entityProperty.Name, CType(entityProperty.GetValue(entity), Decimal).ToString().Replace(",", ".")))
                Else
                    builder.Append(String.Format(formatXml, entityProperty.Name, entityProperty.GetValue(entity)))
                End If
            End If
        Next
        'Propiedades de Navegacion
        If entity.GetType().GetProperties().Any(Function(o) TypeOf o.GetValue(entity) Is IEnumerable AndAlso Not o.PropertyType.Name.Equals("String") AndAlso Not o.Name.Equals("TimeStamp")) Then
            For Each entityProperty As System.Reflection.PropertyInfo In entity.GetType().GetProperties() _
                .Where(Function(o) TypeOf o.GetValue(entity) Is IEnumerable AndAlso Not o.PropertyType.Name.Equals("String") AndAlso Not o.Name.Equals("TimeStamp"))
                If CType(entityProperty.GetValue(entity), IEnumerable) IsNot Nothing AndAlso CType(entityProperty.GetValue(entity), IEnumerable).OfType(Of Object).Count() > 0 Then
                    For Each item In CType(entityProperty.GetValue(entity), IEnumerable)
                        builder.Append(AsXml(item))
                    Next
                End If
            Next
        End If

        builder.Append("</" & entity.GetType().Name & ">")
        Return builder.ToString()
    End Function

#End Region

End Class
