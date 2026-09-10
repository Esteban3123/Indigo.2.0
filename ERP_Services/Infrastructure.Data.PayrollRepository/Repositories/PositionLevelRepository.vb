Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll

Public Class PositionLevelRepository
    Inherits GenericRepository(Of PositionLevel)
    Implements IPositionLevelRepository

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

    ''' <summary>
    ''' Obtiene un nivel especifico
    ''' </summary>
    ''' <param name="codeDetail">Codigo del nivel</param>
    ''' <returns>Nivel de cargo</returns>
    ''' <remarks></remarks>
    Public Function GetPositionLevel(codeDetail As String, Optional tracking As Boolean = True) As PositionLevel Implements IPositionLevelRepository.GetPositionLevel
        Dim PositionLevel = From e In _context.PositionLevel
           Where e.Code = codeDetail
           Select e
        If PositionLevel.Count > 0 Then
            Dim objPositionLevel = Nothing
            If tracking = False Then
                objPositionLevel = (From e In _context.PositionLevel.AsNoTracking
                                   Where e.Code = codeDetail
                                   Select e).SingleOrDefault
            Else
                objPositionLevel = PositionLevel.SingleOrDefault
            End If
            Return objPositionLevel
        Else
            Return New PositionLevel
        End If
    End Function

    ''' <summary>
    ''' Lista todos los niveles de cargo
    ''' </summary>
    ''' <returns>Lista de niveles de cargo</returns>
    ''' <remarks></remarks>
    Public Function ListAllPositionLevel() As List(Of PositionLevel) Implements IPositionLevelRepository.ListAllPositionLevel
        Dim Busqueda = From e In _context.PositionLevel
                                           Select e

        Return Busqueda.ToList
    End Function

End Class
