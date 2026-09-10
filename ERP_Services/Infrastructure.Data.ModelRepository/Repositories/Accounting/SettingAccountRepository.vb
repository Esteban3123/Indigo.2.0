'***********************************************************************
' Assembly         : Infrastructure.Data.SettingAccountRepository
' Author           : Sergio Fernandez
' Created          : 2014-15-05
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.Data.Base
Imports Domain.Entities
#End Region
Public Class SettingAccountRepository
    Inherits GenericRepository(Of GeneralLedgerSettings)
    Implements ISettingsAccountRepository

#Region "Constructor"
    ''' <summary>
    '''Inicializa la nueva instancia de clase.
    ''' </summary>
    ''' <param name="contex">El contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub
#End Region

#Region "Fields"

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork
#End Region

#Region "functions"

    ''' <summary>
    ''' funcion para obtener el parametro contable
    ''' </summary>
    ''' <returns></returns>
    Public Function GetSettingAccount(ByVal idOperationUnit As Integer) As GeneralLedgerSettings Implements ISettingsAccountRepository.GetSettingAccount
        Dim res = (From e In _context.GeneralLedgerSettings.Include("MainAccounts").Include("JournalVoucherTypes").Include("RetentionConcepts").Include("ThirdParty")
                       Where e.IdOperatingUnit = idOperationUnit
                    Select e).FirstOrDefault
        If res IsNot Nothing Then

            Dim mainAccount As MainAccounts

            mainAccount = (From ma In _context.MainAccounts.AsNoTracking Where ma.Id = res.IdDeficitAccount Select ma).FirstOrDefault
            res.IdDeficitAccountDescription = mainAccount.Number + " - " + mainAccount.Name

            mainAccount = (From ma In _context.MainAccounts.AsNoTracking Where ma.Id = res.IdSuperavitAccount Select ma).FirstOrDefault
            res.IdSuperavitAccountDescription = mainAccount.Number + " - " + mainAccount.Name

            mainAccount = (From ma In _context.MainAccounts.AsNoTracking Where ma.Id = res.IdUtilityAccount Select ma).FirstOrDefault
            res.IdUtilityAccountDescription = mainAccount.Number + " - " + mainAccount.Name

            Dim journalVoucherType As JournalVoucherTypes

            journalVoucherType = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.IdCloseDocument Select jvt).FirstOrDefault
            res.IdCloseDocumentDescription = journalVoucherType.Code + " - " + journalVoucherType.Name

            journalVoucherType = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.IdApprovalDocument Select jvt).FirstOrDefault
            res.IdApprovalDocumentDescription = journalVoucherType.Code + " - " + journalVoucherType.Name

            journalVoucherType = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.IdMovementDocument Select jvt).FirstOrDefault
            res.IdMovementDocumentDescription = journalVoucherType.Code + " - " + journalVoucherType.Name

            Dim thirdParty As ThirdParty

            thirdParty = (From tp In _context.ThirdParty.AsNoTracking Where tp.Id = res.IdDian Select tp).FirstOrDefault
            res.IdDianDescription = thirdParty.Nit + " - " + thirdParty.Name

            thirdParty = (From tp In _context.ThirdParty.AsNoTracking Where tp.Id = res.IdDistrictTreasury Select tp).FirstOrDefault
            res.IdDistrictTreasuryDescription = thirdParty.Nit + " - " + thirdParty.Name

            Dim retentionConcept As RetentionConcepts

            retentionConcept = (From rc In _context.RetentionConcepts.AsNoTracking Where rc.Id = res.IdIvaRetentionConcept Select rc).FirstOrDefault
            res.IdIvaRetentionConceptDescription = retentionConcept.Code + " - " + retentionConcept.Name

            retentionConcept = (From rc In _context.RetentionConcepts.AsNoTracking Where rc.Id = res.IdIcaRetentionConcept Select rc).FirstOrDefault
            res.IdIcaRetentionConceptDescription = retentionConcept.Code + " - " + retentionConcept.Name

            retentionConcept = (From rc In _context.RetentionConcepts.AsNoTracking Where rc.Id = res.IdSourceRetentionConcept Select rc).FirstOrDefault
            res.IdSourceRetentionConceptDescription = retentionConcept.Code + " - " + retentionConcept.Name

            res.OriginalValue = (From d In _context.GeneralLedgerSettings.AsNoTracking() Select d).FirstOrDefault()
            Return res
        Else
            Return New GeneralLedgerSettings
        End If
    End Function

    ''' <summary>
    ''' funcion para obtener el parametro contable sin agregados
    ''' </summary>
    ''' <returns></returns>
    Public Function GetSettingAccountSimple(ByVal idOperationUnit As Integer) As GeneralLedgerSettings Implements ISettingsAccountRepository.GetSettingAccountSimple
        Return (From e In _context.GeneralLedgerSettings.AsNoTracking()
                Where e.IdOperatingUnit = idOperationUnit
                Select e).FirstOrDefault
    End Function

    ''' <summary>
    ''' función para obtener los parametros de contabilidad si manejan nomima electrónica
    ''' </summary>
    ''' <returns></returns>
    Public Function GetSettingElectronicPayrollInformation() As GeneralLedgerSettings Implements ISettingsAccountRepository.GetSettingElectronicPayrollInformation
        Return (From e In _context.GeneralLedgerSettings.AsNoTracking()
                Where e.HandlesElectronicPayroll = True
                Select e).FirstOrDefault
    End Function

    ''' <summary>
    ''' función para saber si el empleador maneja nómina electrónica en alguna unidad operativa
    ''' </summary>
    ''' <returns></returns>
    Public Function EmployerHandlesElectronicPayroll(ByVal idDian As Integer) As Boolean Implements ISettingsAccountRepository.EmployerHandlesElectronicPayroll
        Return (From e In _context.GeneralLedgerSettings.AsNoTracking()
                Where e.IdDian = idDian AndAlso e.HandlesElectronicPayroll = True
                Select e).Any()
    End Function
#End Region

End Class