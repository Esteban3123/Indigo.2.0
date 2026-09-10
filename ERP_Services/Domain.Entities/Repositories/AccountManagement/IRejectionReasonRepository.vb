'************************************************************
' Assembly         : Domain.Entities
' Author           : Andrés Steven Rojas
' Created          : 07-01-2025
'
' Copyright        : (c) . All rights reserved.
'************************************************************
Imports Domain.Base

Public Interface IRejectionReasonRepository
    Inherits IRepository(Of RejectionReason)
    ''' <summary>
    ''' Lista los Motivos de Rechazo de acuerdo al usuario
    ''' </summary>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Function ListRejectionReasonByUserCode(ByVal userCode As String) As List(Of RejectionReason)
    ''' <summary>
    ''' Obtiene el Motivo de Rechazo por Id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Function GetRejectionReasonById(ByVal id As Integer) As RejectionReason
    ''' <summary>
    ''' Obtiene el Motivo de Rechazo por Código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Function GetRejectionReasonByCode(ByVal code As String) As RejectionReason

End Interface
