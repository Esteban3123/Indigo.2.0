'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 22-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.InteropCost.Entities

Public Interface IMainAccountRepository
    Inherits IRepository(Of CTNCUENTA)

    ''' <summary>
    ''' Obtiene un profesional de la salud por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetMainAccountByCode(code As String) As CTNCUENTA

    ''' <summary>
    ''' Obtiene unca cuenta contable del erp por id
    ''' </summary>
    Function GetMainAccountById(oid As Integer) As CTNCUENTA

End Interface
