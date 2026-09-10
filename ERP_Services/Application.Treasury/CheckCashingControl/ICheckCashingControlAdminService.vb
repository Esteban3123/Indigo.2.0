'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Hector Rodriguez Rubiano
' Created          : 24-10-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ICheckCashingControlAdminService
    Inherits IDisposable
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="oCheckCashingControl"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function SaveCheckCashingControl(ByVal oCheckCashingControl As CheckCashingControl, ByVal audit As AuditMessage) As ActionResult(Of CheckCashingControl)
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Function GetCheckCashingControlById(ByVal id As Integer) As CheckCashingControl

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="parameters"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Function ListCheckCashingControl(ByVal parameters As String, ByVal session As SessionValues) As DataSet
End Interface
