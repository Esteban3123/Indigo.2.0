'***********************************************************************
' Assembly         : Domain.Entities
' Author           : Diego Andres Roldan Lozano
' Created          : 10-11-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class TreasuryNoteDetail
    Inherits Entity(Of TreasuryNoteDetail)

#Region "Properties"
    <DataMember()>
    Property FullNameThird As String
    <DataMember()>
    Property FullNameCostCenter As String
    <DataMember()>
    Property FullNameNature As String
    <DataMember()>
    Property FullNameMainAccount As String
    <DataMember()>
    Property NoteConceptCode As String
    <DataMember()>
    Property NoteConceptName As String
    <DataMember()>
    Property CodeNameCashFlowConcept As String
#End Region

End Class
