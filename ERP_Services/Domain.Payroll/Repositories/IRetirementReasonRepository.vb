'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 17-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IRetirementReasonRepository
    Inherits IRepository(Of RetirementReason)

    ''' <summary>
    ''' Lista todos los Tipos de Retiros
    ''' </summary>
    ''' <returns>Lista los Tipos de Retiro</returns>
    Function ListAllRetirementReason() As List(Of RetirementReason)

    ''' <summary>
    ''' Obtiene un Tipo de Retiro
    ''' </summary>
    ''' <param name="code">Código del Tipo de Retiro</param>
    ''' <returns>Tipo de Retiro</returns>
    Function GetRetirementReason(ByVal code As String, Optional tracking As Boolean = True) As RetirementReason


    ''' <summary>
    ''' Obtiene un Tipo de Retiro
    ''' </summary>
    ''' <param name="code">Código del Tipo de Retiro</param>
    ''' <returns>Tipo de Retiro</returns>
    Function GetRetirementReasonById(ByVal id As Integer) As RetirementReason
End Interface
