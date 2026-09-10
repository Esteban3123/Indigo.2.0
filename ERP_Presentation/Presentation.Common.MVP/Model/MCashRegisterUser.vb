'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 01-04-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports Presentation.Base
Imports System.ServiceModel
Imports Presentation.Security.MVP
Imports Domain.Security.Entities

#End Region

Public Class MCashRegisterUser
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigoSessionValues As SessionValues

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(ByVal tag As String)
        Me._tagForm = tag
        Me._indigoSessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Gets the cash register user by identifier.
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Async Function GetCashRegisterUserById(ByVal Id As Integer) As Task(Of CashRegisterUser)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetCashRegisterUserByIdAsync(Id, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Lists the cash register user by identifier cash register.
    ''' </summary>
    ''' <param name="IdCashRegister">The identifier cash register.</param>
    ''' <returns></returns>
    Public Async Function ListCashRegisterUserByIdCashRegister(ByVal IdCashRegister As String) As Task(Of List(Of CashRegisterUser))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.ListCashRegisterUserByIdCashRegisterAsync(IdCashRegister, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Lists the user by identifier cash register.
    ''' </summary>
    ''' <param name="IdCashRegister">The identifier cash register.</param>
    ''' <returns></returns>
    Public Async Function ListUserByIdCashRegister(ByVal IdCashRegister As String) As Task(Of List(Of Domain.Security.Entities.User))
        Dim UserCash As List(Of CashRegisterUser) = Await ListCashRegisterUserByIdCashRegister(IdCashRegister)
        Dim ListUser As New List(Of Domain.Security.Entities.User)
        For Each UserCashReg As CashRegisterUser In UserCash
            Using ModeloUsuario As New MUsuario()
                Dim user As Domain.Security.Entities.User = Await ModeloUsuario.GetUserById(CStr(UserCashReg.IdUser))
                ListUser.Add(User)
            End Using
        Next
        Return ListUser
    End Function

    ''' <summary>
    ''' Lists the entity bank account user by identifier entity banki account.
    ''' </summary>
    ''' <param name="CashRegisterId">The identifier entity bank account.</param>
    ''' <returns></returns>
    Public Async Function ListUsersByCashRegisterId(ByVal CashRegisterId As Integer) As Task(Of ActionResult(Of List(Of Domain.Security.Entities.User)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.ListUsersByCashRegisterIdAsync(CashRegisterId, Me._indigoSessionValues.AuditMessageWcf)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: desechar estado administrado (objetos administrados).
            End If

            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: invalidar Finalize() sólo si la instrucción Dispose(ByVal disposing As Boolean) anterior tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Ponga el código de limpieza en la instrucción Dispose(ByVal disposing As Boolean) anterior.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agregó este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
