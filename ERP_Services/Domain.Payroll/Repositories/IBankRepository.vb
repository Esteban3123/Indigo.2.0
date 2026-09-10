'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 21-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IBankRepository
    Inherits IRepository(Of Bank)

    ''' <summary>
    ''' Lista todos los bancos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllBank() As List(Of Bank)

    ''' <summary>
    ''' Obtiene un Banco especifico
    ''' </summary>
    ''' <param name="code">Codigo del Banco</param>
    ''' <returns>Banco</returns>
    ''' <remarks></remarks>
    Function GetBank(ByVal code As String, Optional tracking As Boolean = True) As Bank

    ''' <summary>
    ''' Obtiene un banco por el identificador
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetBankById(ByVal Id As Integer, Optional tracking As Boolean = True) As Bank

    ''' <summary>
    ''' Sp para la importación de archivo
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <returns></returns>
    Function SetBankDetail(xml As String) As Entity.Core.Objects.ObjectResult(Of SP_SetBankDetail_Result)

    ''' <summary>
    ''' Copiar y pegar para Reglas de reconocimiento Automático
    ''' </summary>
    ''' <param name="XmlObject"></param>
    ''' <returns></returns>
    Function SetBankAutomaticRecognitionRules(xmlObject As String) As List(Of SP_SetBankAutomaticRecognitionRules_Result)

End Interface
