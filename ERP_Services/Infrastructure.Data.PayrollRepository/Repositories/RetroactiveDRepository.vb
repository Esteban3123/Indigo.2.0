'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Oscar stiven astudillo reyes
' Created          : 2025-10-16
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports Infrastructure.Data.Base

Public Class RetroactiveDRepository
    Inherits GenericRepository(Of RetroactiveD)

    Implements IRetroactiveDRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IPayrollUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IPayrollUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

End Class
