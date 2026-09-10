'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Daniel Eduardo Arévalo
' Created          : 08/09/2017
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure

Public Class AgreementsMassiveRepository

    Inherits GenericRepository(Of JournalVouchers)
    Implements IAgreementsMassiveRepository

    ''' <summary>
    ''' Contexto de payrrol
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payrrol
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Importa el archivo de excel y valida los datos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_ImportFileAgreementsMassive(data As String) As List(Of SP_ImportFileAgreementsC_Result) Implements IAgreementsMassiveRepository.SP_ImportFileAgreementsMassive
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ImportFileAgreementsC(data).ToList()
    End Function

    ''' <summary>
    ''' Guarda la informacion de los saldos iniciales
    ''' </summary>
    ''' <param name="Data"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Public Function SP_SaveAgreementsMassive(Data As String, CodeUser As String) As List(Of SP_SaveAgreementsCMassive_Result) Implements IAgreementsMassiveRepository.SP_SaveAgreementsMassive
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveAgreementsCMassive(Data, CodeUser).ToList()
    End Function
End Class
