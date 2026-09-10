'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Cristhian Mauricio Salazar
' Created          : 27-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.Data.Base

Public Class KinshipRepository
    Inherits GenericRepository(Of Kinship)
    Implements IKinshipRepository

    ' Contexto de payroll
    Private _contex As IPayrollUnitOfWork

    Public Sub New(ByVal contexto As IPayrollUnitOfWork)
        MyBase.New(contexto)
        _contex = contexto
    End Sub

    ''' <summary>
    ''' Obtiene un parentesco especifico
    ''' </summary>
    ''' <param name="code">Codigo del parentesco</param>
    ''' <returns>Parentesco</returns>
    ''' <remarks></remarks>
    Public Function GetKinship(code As String, Optional tracking As Boolean = True) As Kinship Implements IKinshipRepository.GetKinship
        Dim kinship = From e In _contex.Kinship
                      Where e.Code = code
                      Select e
        If kinship.Count > 0 Then
            Dim objkinship = Nothing
            If tracking = False Then
                objkinship = (From e In _contex.Kinship.AsNoTracking
                              Where e.Code = code
                              Select e).SingleOrDefault
            Else
                objkinship = kinship.SingleOrDefault
            End If
            Return objkinship
        Else
            Return New Kinship()
        End If
    End Function

    ''' <summary>
    ''' Obtiene todos los parentescos
    ''' </summary>
    ''' <returns>Lista de parentescos</returns>
    ''' <remarks></remarks>
    Public Function ListAllKinship() As List(Of Kinship) Implements IKinshipRepository.ListAllKinship
        Dim kinship = From e In _contex.Kinship
                      Select e
        Return kinship.ToList()
    End Function
End Class
