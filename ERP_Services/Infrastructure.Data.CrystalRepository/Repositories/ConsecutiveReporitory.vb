'***********************************************************************
' Assembly         : Infrastructure.Data.CrystalRepository
' Author           : Jhossept K. Garay Rodriguez
' Created          : 19-03-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Crystal
Imports Domain.Crystal.Entities
Imports System.Dynamic
Imports Infrastructure.CrossCutting.Resources

Public Class ConsecutiveReporitory
    Inherits GenericRepository(Of INCONSECU)
    Implements IConsecutiveRepository

    'Contexto del repositorio de Indigo Vie Cloud Platform
    Private _crystalContext As ICrystalModelUnitOfWork

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="crystalContext">Contexto</param>
    Public Sub New(ByVal crystalContext As ICrystalModelUnitOfWork)
        MyBase.New(crystalContext)
        Me._crystalContext = crystalContext
    End Sub

    ''' <summary>
    ''' Obtiene el consecutivo de ingreso
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetConsecutive(codigoConsecutivo As String) As INCONSECU Implements IConsecutiveRepository.GetConsecutive
        Dim result = From e In _crystalContext.INCONSECU
                    Where e.IDCONSECU = codigoConsecutivo
                    Select e

        If result.Count > 0 Then
            Return result.SingleOrDefault()
        Else
            Return Nothing
        End If
    End Function
End Class
