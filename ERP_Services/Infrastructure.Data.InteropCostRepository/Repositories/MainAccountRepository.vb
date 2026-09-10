'***********************************************************************
' Assembly         : Infrastructure.Data.CrystalRepository
' Author           : Cristhian Mauricio Salazar
' Created          : 03-11-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.InteropCost
Imports Domain.InteropCost.Entities

Public Class MainAccountRepository
    Inherits GenericRepository(Of CTNCUENTA)
    Implements IMainAccountRepository

    ' contexto del repositorio de ciudades
    Private _context As IInteropCostModelUnitOfWork

    ''' <summary>
    '''  Contructor del repositorio coidades el cual instancia una nueva clase
    ''' </summary>
    ''' <param name="context">contexto del repositorio</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IInteropCostModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un profesional de la salud por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMainAccountByCode(code As String) As CTNCUENTA Implements IMainAccountRepository.GetMainAccountByCode
        Dim query = From e In _context.CTNCUENTA.AsNoTracking()
                    Where e.CUECODIGO = code
                    Select e
        If query.Count > 0 Then
            Return query.SingleOrDefault()
        Else
            Return New CTNCUENTA()
        End If
    End Function

    ''' <summary>
    ''' Obtiene unca cuenta contable del erp por id
    ''' </summary>
    ''' <param name="oid"></param>
    ''' <returns></returns>
    Public Function GetMainAccountById(oid As Integer) As CTNCUENTA Implements IMainAccountRepository.GetMainAccountById
        Dim query = From e In _context.CTNCUENTA.AsNoTracking()
                    Where e.OID = oid
                    Select e
        If query.Count > 0 Then
            Return query.FirstOrDefault()
        Else
            Return New CTNCUENTA()
        End If
    End Function

End Class
