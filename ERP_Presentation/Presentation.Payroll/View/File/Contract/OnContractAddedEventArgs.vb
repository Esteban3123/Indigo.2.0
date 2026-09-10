'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Jose Luis Rojas
' Created          : 12-08-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Payroll.Entities

''' <summary>
''' Clase que tiene los argumentos que se devuelven al dar click en el boton añadir, hereda de EvenArgs para que se devuelve en el evento
''' </summary>
Public Class OnContractAddedEventArgs
    Inherits EventArgs

    Private _employee As Employee
    Public Property Employee() As Employee
        Get
            Return _employee
        End Get
        Private Set(ByVal value As Employee)
            _employee = value
        End Set
    End Property

    Public Sub New(emp As Employee)
        Employee = emp
    End Sub

End Class