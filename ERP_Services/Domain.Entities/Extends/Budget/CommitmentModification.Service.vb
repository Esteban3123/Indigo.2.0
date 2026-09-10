#Region "imports"
Imports System.Runtime.Serialization
#End Region

Partial Public Class CommitmentModification

#Region "Properties"

    ''' <summary>
    ''' obtiene o establece el id de la unidad operativa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property OperatingUnitId As Integer

    ''' <summary>
    ''' Obtiene o establece el codigo del compromiso
    ''' </summary>
    <DataMember()>
    Public Property CodeCommitment As String

    ''' <summary>
    ''' Obtiene o establece el tipo de compromiso
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property CommitmentType As Integer

#End Region

End Class
