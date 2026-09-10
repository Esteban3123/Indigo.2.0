'***********************************************************************
' Assembly         : Presentacion.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 28/05/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.Data.Xpo.BillingRepository

#End Region

Public Class SelectContractEventArgs
    Inherits EventArgs

    ''' <summary>
    ''' Obtiene o establece el id del contrato seleccionado
    ''' </summary>
    ''' <remarks></remarks>
    Public MedicalFeesContractId As Integer

    ''' <summary>
    ''' Obtiene o establece el porcentaje que se le aplica al item seleccionado
    ''' </summary>
    ''' <remarks></remarks>
    Public Percentage As Decimal

End Class
