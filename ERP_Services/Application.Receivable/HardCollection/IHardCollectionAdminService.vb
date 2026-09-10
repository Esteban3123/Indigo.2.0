'***********************************************************************
' Assembly         : Application.Portfolio
' Author           : Diego Andrés Roldán Lozano
' Created          : 29-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IHardCollectionAdminService
    Inherits IDisposable
    Function GetHardCollectionByCode(code As String, ByVal audit As AuditMessage) As HardCollection

    Function SaveHardCollection(HardCollection As HardCollection, ByVal audit As AuditMessage) As ActionResult(Of HardCollection)

    Function GetHardCollectionDetailByHardCollectionId(hardCollectionId As Integer) As List(Of HardCollectionDetail)

    Function SetCopyPasteOrImportFileHardCollection(dataImportFile As List(Of ImportFileRow), dataCopyPaste As List(Of List(Of String))) As ActionResult(Of List(Of HardCollectionDetail))
End Interface
