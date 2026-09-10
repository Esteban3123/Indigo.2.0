'***********************************************************************
' Assembly         : Domain.Base
' Author           : Juan F. Tamayo
' Created          : 2013-10-07
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Runtime.Serialization
Imports Futesh.Documents

#End Region

''' <summary>
''' Encapsula los datos de un documento que se almacenará
''' o se encuentra almacenado en el motror de indexación
''' </summary>
<Serializable(), DataContract(IsReference:=True), Obsolete("Debe usar IndexedDocument2", True)>
Public Class IndexedDocument

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna el id interno del documento
    ''' </summary>
    ''' <value>Id del documento</value>
    ''' <returns>El Id del documento</returns>
    <DataMember()>
    Public Property Id As Int64

    ''' <summary>
    ''' Obtiene o asigna el Id de la base de documentos
    ''' en la que se encuentra almacenado el documento
    ''' </summary>
    ''' <value>Id de la base</value>
    ''' <returns>El Id de la base</returns>
    <DataMember()>
    Public Property IdDb As Int64

    ''' <summary>
    ''' Obtiene o asigna la fecha de indexación del documento
    ''' </summary>
    ''' <value>Fecha de indexación del documento</value>
    ''' <returns>La fecha de indexación del documento</returns>
    <DataMember()>
    Public Property IndexingDate As DateTime

    ''' <summary>
    ''' Obtiene o asigna el arreglo de indices interno del documento
    ''' </summary>
    ''' <value>Arreglo de indices internos</value>
    ''' <returns>El arreglo de indices interno</returns>
    <DataMember()>
    Public Property Indexes As String()

    ''' <summary>
    ''' Obtiene o asigna el Id de la entidad
    ''' </summary>
    ''' <value>Id de la entidad</value>
    ''' <returns>El Id de la entidad</returns>
    <DataMember(), Field("IdEntity", IndexType:=Field.IndexType.Analyzed)>
    Public Property IdEntity As String

    ''' <summary>
    ''' Obtiene o asigna el Id o Tag del frontal
    ''' </summary>
    ''' <value>Id o Tag del frontal</value>
    ''' <returns>El Id o Tag del frontal</returns>
    <DataMember(), Field("IdForm", IndexType:=Field.IndexType.NotAnalyzed)>
    Public Property IdForm As String

    ''' <summary>
    ''' Obtiene o asigna el nombre del frontal
    ''' </summary>
    ''' <value>Nombre del frontal</value>
    ''' <returns>El nombre del frontal</returns>
    ''' <remarks>Esta propiedad es manual y no se serializa para el transporte entre servicios</remarks>
    Public Property NameForm As String

    ''' <summary>
    ''' Obtiene o asigna el tipo de documento
    ''' </summary>
    ''' <value>Tipo de documento</value>
    ''' <returns>El tipo del documento</returns>
    <DataMember(), Field("DocumentType", IndexType:=Field.IndexType.NotAnalyzed)>
    Public Property DocumentType As Int32

    ''' <summary>
    ''' Obtiene o asigna la extensión del adocumento si éste es digitalizado
    ''' </summary>
    ''' <value>Estensión del documento</value>
    ''' <returns>La extensión del documento</returns>
    <DataMember(), Field("Extension", IndexType:=Field.IndexType.NotAnalyzed)>
    Public Property Extension As String

    ''' <summary>
    ''' Obtiene o asigna la fecha de creación del documento
    ''' </summary>
    ''' <value>Fecha de creación</value>
    ''' <returns>La fecha de creación</returns>
    <DataMember(), Field("CreationDate", IndexType:=Field.IndexType.NotAnalyzed)>
    Public Property CreationDate As DateTime

    ''' <summary>
    ''' Obtiene o asigna la fecha de actualización
    ''' </summary>
    ''' <value>Fecha de actualización</value>
    ''' <returns>La fecha de actualización</returns>
    <DataMember(), Field("Update", IndexType:=Field.IndexType.NotAnalyzed)>
    Public Property Update As DateTime

    ''' <summary>
    ''' Obtiene o asigna el nombre del usuario creador
    ''' </summary>
    ''' <value>Nombre del usuario creador</value>
    ''' <returns>El nombre del usuario creador</returns>
    <DataMember(), Field("CreationUser", IndexType:=Field.IndexType.NotAnalyzed)>
    Public Property CreationUser As String

    ''' <summary>
    ''' Obtiene o asigna el nombre del usuario actualizador
    ''' </summary>
    ''' <value>Nombre del usuario actualizador</value>
    ''' <returns>El nombre del usuario actualizador</returns>
    <DataMember(), Field("UpdateUser", IndexType:=Field.IndexType.NotAnalyzed)>
    Public Property UpdateUser As String

    ''' <summary>
    ''' Obtiene o asigna el titulo del documento
    ''' </summary>
    ''' <value>Titulo del documento</value>
    ''' <returns>El titulo del documento</returns>
    <DataMember(), Field("Title", IndexType:=Field.IndexType.Analyzed)>
    Public Property Title As String

    ''' <summary>
    ''' Obtiene o asigna el contenido del documento
    ''' </summary>
    ''' <value>Contenido del documento</value>
    ''' <returns>El contenido del documento</returns>
    <DataMember(), Field("Content", IndexType:=Field.IndexType.Analyzed)>
    Public Property Content As String

#End Region

#Region "Methods"

    ''' <summary>
    ''' Convierte el objeto en su correspondiente DataTable
    ''' </summary>
    ''' <returns>DataTable del objeto</returns>
    Public Function ToDataTable() As DataTable
        Dim dt As New DataTable("IndexedDocument")
        With dt
            .Columns.Add("Id", GetType(Int64))
            .Columns.Add("IdDb", GetType(Int64))
            .Columns.Add("Indexes", GetType(String()))
            .Columns.Add("IndexingDate", GetType(DateTime))
            .Columns.Add("IdEntity", GetType(String))
            .Columns.Add("IdForm", GetType(String))
            .Columns.Add("DocumentType", GetType(Int32))
            .Columns.Add("Extension", GetType(String))
            .Columns.Add("CreationDate", GetType(DateTime))
            .Columns.Add("Update", GetType(DateTime))
            .Columns.Add("CreationUser", GetType(String))
            .Columns.Add("UpdateUser", GetType(String))
            .Columns.Add("Title", GetType(String))
            .Columns.Add("Content", GetType(String))
        End With
        Dim dtRow As DataRow = dt.NewRow()
        With dtRow
            .Item("Id") = Me.Id
            .Item("IdDb") = Me.IdDb
            If Me.Indexes IsNot Nothing Then
                .Item("Indexes") = Me.Indexes
            Else
                .Item("Indexes") = New String() {}
            End If
            .Item("IndexingDate") = Me.IndexingDate
            If Me.IdEntity IsNot Nothing Then
                .Item("IdEntity") = Me.IdEntity.Trim()
            Else
                .Item("IdEntity") = String.Empty
            End If
            If Me.IdForm IsNot Nothing Then
                .Item("IdForm") = Me.IdForm.Trim()
            Else
                .Item("IdForm") = String.Empty
            End If
            If Not Object.Equals(Me.DocumentType, Nothing) Then
                .Item("DocumentType") = Me.DocumentType
            Else
                .Item("DocumentType") = IndexedDocumentType.File
            End If
            If Me.Extension IsNot Nothing Then
                If Me.Extension.Trim().Length > 1 Then
                    .Item("Extension") = Me.Extension.Trim().Substring(1)
                Else
                    .Item("Extension") = Me.Extension.Trim()
                End If
            Else
                .Item("Extension") = String.Empty
            End If
            .Item("CreationDate") = Me.CreationDate
            .Item("Update") = Me.Update
            If Me.CreationUser IsNot Nothing Then
                .Item("CreationUser") = Me.CreationUser.Trim()
            Else
                .Item("CreationUser") = String.Empty
            End If
            If Me.UpdateUser IsNot Nothing Then
                .Item("UpdateUser") = Me.UpdateUser.Trim()
            Else
                .Item("UpdateUser") = String.Empty
            End If
            If Me.Title IsNot Nothing Then
                .Item("Title") = Me.Title.Trim()
            Else
                .Item("Title") = String.Empty
            End If
            If Me.Content IsNot Nothing Then
                .Item("Content") = Me.Content.Trim()


















































































































































































































































































































            Else
                .Item("Content") = String.Empty
            End If
        End With
        dt.Rows.Add(dtRow)
        Return dt
    End Function

#End Region

End Class

''' <summary>
''' Tipo de documento indexado
''' </summary>
<DataContract()>
Public Enum IndexedDocumentType

    ''' <summary>
    ''' Representa un documento indexado por medio de un
    ''' frontal de archivo
    ''' </summary>
    <EnumMember()> _
    File = 1
    ''' <summary>
    ''' Representa un documento indexado por medio de un
    ''' frontal de proceso
    ''' </summary>
    <EnumMember()> _
    Process = 2
    ''' <summary>
    ''' Representa un documento indexado por medio de un
    ''' proceso de digitalización
    ''' </summary>
    <EnumMember()> _
    ScannedDocument = 3

End Enum