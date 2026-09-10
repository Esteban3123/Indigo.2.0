'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 08/09/2017
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities
Imports Domain.Base.Entities

Public Interface IAgreementsMassiveRepository
    Inherits IRepository(Of JournalVouchers)

    ''' <summary>
    ''' Importa el archivo de excel y valida los datos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_ImportFileAgreementsMassive(data As String) As List(Of SP_ImportFileAgreementsC_Result)

    ''' <summary>
    ''' Guarda la información de saldo inicial
    ''' </summary>
    ''' <param name="Data"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Function SP_SaveAgreementsMassive(Data As String, CodeUser As String) As List(Of SP_SaveAgreementsCMassive_Result)
End Interface
