'***********************************************************************
' Assembly         : Domain.Base
' Author           : Juan F. Tamayo
' Created          : 2013-10-25
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Runtime.Serialization
Imports Futesh.Documents
Imports Futesh.Index
Imports MongoDB.Bson

#End Region

''' <summary>
''' Encapsula un conjunto de documentos indexados como resultado de una busqueda
''' </summary>
<DataContract(IsReference:=True), KnownType(GetType(IndexedDocument2)), KnownType(GetType(IndexedDocumentType)), KnownType(GetType(ObjectId))>
Public Class IndexedDocumentResultSet2

#Region "Fields"

    ''' <summary>
    ''' Lista de documentos indexados
    ''' </summary>
    Private _results As List(Of IndexedDocument2)

    ''' <summary>
    ''' Cantidad de resultados en crudo
    ''' </summary>
    Private _rawCount As Int64

    ''' <summary>
    ''' Tiempo transcurrido en la consulta
    ''' </summary>
    Private _elapsedTime As TimeSpan

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna la lista de documentos indexados
    ''' </summary>
    ''' <value>Lista de documentos indexados</value>
    ''' <returns>La lista de documentos indexados</returns>
    <DataMember()>
    Public Property Results As List(Of IndexedDocument2)
        Get
            Return Me._results
        End Get
        Set(value As List(Of IndexedDocument2))
            Me._results = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la cantidad de resultados en crudo
    ''' </summary>
    ''' <value>Cantidad de resultados en crudo</value>
    ''' <returns>La cantidad de resultados en crudo</returns>
    <DataMember()>
    Public Property RawCount As Int64
        Get
            Return Me._rawCount
        End Get
        Set(value As Int64)
            Me._rawCount = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el tiempo transcurrido en la consulta
    ''' </summary>
    ''' <value>Tiempo transcurrido en la consulta</value>
    ''' <returns>El tiempo transcurrido en la consulta</returns>
    <DataMember()>
    Public Property ElapsedTime As TimeSpan
        Get
            Return Me._elapsedTime
        End Get
        Set(value As TimeSpan)
            Me._elapsedTime = value
        End Set
    End Property

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New()
        Me._results = New List(Of IndexedDocument2)()
        Me._rawCount = 0
        Me._elapsedTime = New TimeSpan()
    End Sub

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="results">Lista de resultados</param>
    ''' <param name="rawCount">Cantidad de resultados en crudo</param>
    ''' <param name="elapsedTime">Tiempo transcurrido en la consulta</param>
    Friend Sub New(ByVal results As List(Of IndexedDocument2), ByVal rawCount As Int64, ByVal elapsedTime As TimeSpan)
        Me._results = results
        Me._rawCount = rawCount
        Me._elapsedTime = elapsedTime
    End Sub

#End Region

End Class
