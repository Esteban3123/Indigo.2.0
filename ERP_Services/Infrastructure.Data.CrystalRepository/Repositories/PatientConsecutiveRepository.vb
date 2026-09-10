'***********************************************************************
' Assembly         : Infrastructure.Data.CrystalRepository
' Author           : Jhossept K. Garay Rodriguez
' Created          : 07-02-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Crystal
Imports Domain.Crystal.Entities
Imports Domain.Entities
Imports System.Dynamic
Imports Infrastructure.CrossCutting.Resources

Public Class PatientConsecutiveRepository
    Inherits GenericRepository(Of INCONSEPA)
    Implements IPatientConsecutiveRepository

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
    ''' Funcion para obtener el consecutivo segun tipo de población y documento
    ''' </summary>
    ''' <param name="PoblationType"></param>
    ''' <param name="Document"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetConsecutiveByPoblationTypeAndDocument(PoblationType As String, Document As String) As INCONSEPA Implements IPatientConsecutiveRepository.GetConsecutiveByPoblationTypeAndDocument
        Dim res = From e In _crystalContext.INCONSEPA
                  Where e.IDPOBLACI = PoblationType And e.IDDOCUMEN = Document
                  Select e

        If res.Count > 0 Then
            Return res.SingleOrDefault
        Else
            Return New INCONSEPA
        End If
    End Function
End Class
