'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 06-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities
Public Interface IPositionRepository
    Inherits IRepository(Of Position)

    ''' <summary>
    ''' Lista todos los cargos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllPosition() As List(Of Position)

    ''' <summary>
    ''' Obtiene un cargo especifico
    ''' </summary>
    ''' <param name="code">Codigo del cargo</param>
    ''' <returns>Cargo</returns>
    ''' <remarks></remarks>
    Function GetPosition(ByVal code As String, Optional tracking As Boolean = True) As Position

    Function GetPositionById(ByVal IdPosition As Integer) As Position

End Interface
