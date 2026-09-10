'************************************************************
' Assembly         : Domain.Billing
' Author           : Juan Carlos Bermudez Gutierrez
' Created          : 05-01-2015
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface ISlipOutRepository
    Inherits IRepository(Of SlipOut)

    ''' <summary>
    ''' Obtiene una boleta de salida por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSlipOutByCode(code As String) As SlipOut

    ''' <summary>
    ''' Obtiene una boleta de salida por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSlipOutById(Id As Integer) As SlipOut

    ''' <summary>
    ''' Obtiene una boleta de salida por numero de admisión
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSlipOutByAdmissionNumber(admissionNumber As String) As SlipOut


End Interface
