'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 08/09/2017
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities

Public Interface IAgreementsMassiveAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Importa el archivo de excel y valida los datos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_ImportFileAgreementsMassive(data As List(Of ImportFileRow)) As ActionResult(Of List(Of SP_ImportFileAgreementsC_Result))

    ''' <summary>
    ''' Guarda la información de saldo inicial
    ''' </summary>
    ''' <param name="Data"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Function SP_SaveAgreementsMassive(ListInfo As List(Of SP_ImportFileAgreementsC_Result), Audit As AuditMessage) As ActionResult(Of List(Of Tuple(Of String, Integer)))

End Interface
