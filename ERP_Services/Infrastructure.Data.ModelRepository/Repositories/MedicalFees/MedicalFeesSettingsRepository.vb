'***********************************************************************
' Assembly         : Infrastructure.Data.MedicalFeesRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/01/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class MedicalFeesSettingsRepository
    Inherits GenericRepository(Of SettingMedicalFees)
    Implements IMedicalFeesSettingsRepository

#Region "Fields"

    ''' <summary>
    ''' Contexto
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene los parametros de los honorarios medicos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMedicalFeesSetting() As SettingMedicalFees Implements IMedicalFeesSettingsRepository.GetMedicalFeesSetting
        Dim res = (From s In _context.SettingMedicalFees Select s).FirstOrDefault
        If res IsNot Nothing Then

            Dim accountPayableConcept = (From c In _context.AccountPayableConcepts.AsNoTracking Where c.Id = res.AccountPayableConceptId Select c).FirstOrDefault
            res.AccountPayableConceptDescription = accountPayableConcept.Code + " - " + accountPayableConcept.Name

            Dim journalVoucherType = (From j In _context.JournalVoucherTypes.AsNoTracking Where j.Id = res.JournalVoucherTypeId Select j).FirstOrDefault
            res.JournalVoucherTypeDescription = journalVoucherType.Code + " - " + journalVoucherType.Name

            If res.CostRecognitionVoucherId IsNot Nothing Then
                res.CostRecognitionVoucherDescription = (From j In _context.JournalVoucherTypes.AsNoTracking
                                                         Where j.Id = res.CostRecognitionVoucherId
                                                         Select String.Concat(j.Code, " - ", j.Name)).FirstOrDefault
            End If

            If res.CostRecognitionReversalVoucherId IsNot Nothing Then
                res.CostRecognitionReversalVoucherDescription = (From j In _context.JournalVoucherTypes.AsNoTracking
                                                                 Where j.Id = res.CostRecognitionReversalVoucherId
                                                                 Select String.Concat(j.Code, " - ", j.Name)).FirstOrDefault
            End If

            res.OriginalValue = (From s In _context.SettingMedicalFees.AsNoTracking Select s).FirstOrDefault

            Return res
        Else
            Return Nothing
        End If
    End Function

#End Region

End Class
