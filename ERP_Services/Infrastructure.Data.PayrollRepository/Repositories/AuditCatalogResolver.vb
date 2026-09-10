'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Jhon Willian Corredor Araujo
' Created          : 04-09-2026
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities

Public Class AuditCatalogResolver

    Implements IAuditCatalogResolver

    ''' <summary>
    ''' Contexto de payroll
    ''' </summary>
    Private _context As IPayrollUnitOfWork

    ''' <summary>
    ''' Nombres ya resueltos en la operación en curso
    ''' </summary>
    Private ReadOnly _resolved As New Dictionary(Of String, String)

    Public Sub New(ByVal context As IPayrollUnitOfWork)
        _context = context
    End Sub

    Public Function GetName(catalog As AuditCatalog, id As Integer) As String Implements IAuditCatalogResolver.GetName

        If id <= 0 Then
            Return String.Empty
        End If

        Dim key As String = catalog.ToString() & "|" & id.ToString()

        If _resolved.ContainsKey(key) Then
            Return _resolved(key)
        End If

        Dim name As String = QueryName(catalog, id)
        _resolved(key) = name

        Return name

    End Function

    Public Function GetContactText(kind As AuditContactKind, id As Integer) As String Implements IAuditCatalogResolver.GetContactText

        If id <= 0 Then
            Return String.Empty
        End If

        Select Case kind

            Case AuditContactKind.Address
                Return NameOrEmpty((From c In _context.Address Where c.Id = id Select c.Addresss).FirstOrDefault())

            Case AuditContactKind.Phone
                Return NameOrEmpty((From c In _context.Phone Where c.Id = id Select c.Phone1).FirstOrDefault())

            Case AuditContactKind.Email
                Return NameOrEmpty((From c In _context.Email Where c.Id = id Select c.Email1).FirstOrDefault())

            Case Else
                Return String.Empty

        End Select

    End Function

    Private Function QueryName(catalog As AuditCatalog, id As Integer) As String

        Select Case catalog

            Case AuditCatalog.FunctionalUnit
                Return NameOrEmpty((From c In _context.FunctionalUnit Where c.Id = id Select c.Name).FirstOrDefault())

            Case AuditCatalog.Group
                Return NameOrEmpty((From c In _context.Group Where c.Id = id Select c.Name).FirstOrDefault())

            Case AuditCatalog.ContractType
                Return NameOrEmpty((From c In _context.ContractType Where c.Id = id Select c.Name).FirstOrDefault())

            Case AuditCatalog.Position
                Return NameOrEmpty((From c In _context.Position Where c.Id = id Select c.Name).FirstOrDefault())

            Case AuditCatalog.Bank
                Return NameOrEmpty((From c In _context.Bank Where c.Id = id Select c.Name).FirstOrDefault())

            Case AuditCatalog.OrganizationChartPosition
                Return NameOrEmpty((From c In _context.OrganizationChartPosition Where c.Id = id Select c.Name).FirstOrDefault())

            Case AuditCatalog.ContractModificationReason
                Return NameOrEmpty((From c In _context.ContractModificationReason Where c.Id = id Select c.Name).FirstOrDefault())

            Case AuditCatalog.CostCenter
                Return NameOrEmpty((From c In _context.CostCenter Where c.Id = id Select c.Name).FirstOrDefault())

            Case AuditCatalog.WorkCenter
                Return NameOrEmpty((From c In _context.WorkCenter Where c.Id = id Select c.Name).FirstOrDefault())

            Case AuditCatalog.EmployeeType
                Return NameOrEmpty((From c In _context.EmployeeType Where c.Id = id Select c.Name).FirstOrDefault())

            Case AuditCatalog.PensionaryType
                Return NameOrEmpty((From c In _context.PensionaryType Where c.Id = id Select c.Name).FirstOrDefault())

            Case AuditCatalog.ContributorSubtype
                Return NameOrEmpty((From c In _context.ContributorSubtype Where c.Id = id Select c.Name).FirstOrDefault())

            Case AuditCatalog.ReligiousBeliefs
                Return NameOrEmpty((From c In _context.ReligiousBeliefs Where c.Id = id Select c.Description).FirstOrDefault())

            Case AuditCatalog.EthnicGroups
                Return NameOrEmpty((From c In _context.EthnicGroups Where c.Id = id Select c.Description).FirstOrDefault())

            Case AuditCatalog.City
                Return NameOrEmpty((From c In _context.City Where c.Id = id Select c.Name).FirstOrDefault())

            Case Else
                Return String.Empty

        End Select

    End Function

    Private Shared Function NameOrEmpty(value As String) As String

        If String.IsNullOrWhiteSpace(value) Then
            Return String.Empty
        End If

        Return value.Trim()

    End Function

End Class
