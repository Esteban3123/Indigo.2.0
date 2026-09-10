'************************************************************
' Assembly         : Infrastructure.Data.Billing
' Author           : Cristian Camilo Bahamon
' Created          : 2022-07-29
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

Public Class BillingJustificationContolRepository
    Inherits GenericRepository(Of BillingJustificationControl)
    Implements IBillingJustificationControlRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    Public Function ListAllJustificationControl() As List(Of BillingJustificationControl) Implements IBillingJustificationControlRepository.ListAllJustificationControl
        Dim Busqueda = From e In _context.BillingJustificationControl
                       Select e
        Return Busqueda.ToList()
    End Function

    Public Function GetJustificationControl(code As String) As BillingJustificationControl Implements IBillingJustificationControlRepository.GetJustificationControl
        Dim justificationControl = (From e In _context.BillingJustificationControl.Include("BillingJustificationControlUser")
                                    Where e.Code = code
                                    Select e).FirstOrDefault()

        If justificationControl IsNot Nothing Then
            justificationControl.OriginalValue = (From e In _context.BillingJustificationControl.AsNoTracking()
                                                  Where e.Code = code
                                                  Select e).FirstOrDefault()
            Return justificationControl
        Else
            Return New BillingJustificationControl()
        End If
    End Function

    Public Function GetJustificationControlById(idJustificationControl As Integer, Optional tracking As Boolean = True) As BillingJustificationControl Implements IBillingJustificationControlRepository.GetJustificationControlById
        If tracking Then
            Dim justificationControl = (From e In _context.BillingJustificationControl
                                        Where e.Id = idJustificationControl
                                        Select e).FirstOrDefault()

            If justificationControl IsNot Nothing Then
                Return justificationControl
            Else
                Return New BillingJustificationControl()
            End If
        Else
            Dim justificationControl = (From e In _context.BillingJustificationControl.AsNoTracking
                                        Where e.Id = idJustificationControl
                                        Select e).FirstOrDefault()

            If justificationControl IsNot Nothing Then
                Return justificationControl
            Else
                Return New BillingJustificationControl()
            End If
        End If
    End Function



End Class
