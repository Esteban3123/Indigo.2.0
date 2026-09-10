'************************************************************
' Assembly         : Infraestructure.Data.GlosasRepository
' Author           : Juan Diego Diaz
' Created          : 22-05-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

''' <summary>
''' Repositorio pagos parcilaes en conciliacion
''' </summary>
Public Class PartialPaymentsCRepository
    Inherits GenericRepository(Of PartialPaymentsC)
    Implements IPartialPaymentsCRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''Inicializa la nueva instancia de clase.
    ''' </summary>
    ''' <param name="contex">El contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub


    ''' <summary>
    ''' Obtener un oficio de pagos parciales por medio del consecutivo
    ''' </summary>
    ''' <param name="consecutive">consecutivo del oficio</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPartialPaymentsC(consecutive As String) As PartialPaymentsC Implements IPartialPaymentsCRepository.GetPartialPaymentsC
        Dim tmpPartialPaymentsC = From e In _context.PartialPaymentsC.Include("Customer").Include("PartialPaymentsD")
            Where e.RadicatedConsecutive = consecutive
            Select e
        If tmpPartialPaymentsC.Count > 0 Then
            Dim ResponsibleData = tmpPartialPaymentsC.SingleOrDefault
            ResponsibleData.OriginalValue = (From e In _context.PartialPaymentsC.AsNoTracking
         Where e.RadicatedConsecutive = consecutive
         Select e).SingleOrDefault
            Return ResponsibleData
        End If
        Return New PartialPaymentsC
    End Function

End Class
