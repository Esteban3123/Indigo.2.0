'***********************************************************************
' Assembly         : Infrastructure.Data.CrystalRepository
' Author           : Diego Andrés Roldán
' Created          : 13-07-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Crystal
Imports Domain.Crystal.Entities

Public Class ExternalConsultationRepository
    Inherits GenericRepository(Of HCHOGASIN)
    Implements IExternalConsultationRepository

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

    Public Function ListarCitasMedicas(patientCode As String, atentionCenterCode As String) As List(Of SP_AD_ListarCitasMedicasNativo_Result) Implements IExternalConsultationRepository.ListarCitasMedicas
        Dim query = (From cm In _crystalContext.SP_AD_ListarCitasMedicasNativo(patientCode, atentionCenterCode) Where cm.GeneroOrdenServicio = False Select cm).ToList()

        For Each item In query
            item.ACTIVICON = (From A In _crystalContext.AGACTIMED Where A.CODACTMED = item.CodigioActividadMedica Select A.ACTIVICON).FirstOrDefault()
        Next

        'se asigna el codigo de la cita al nuevo extendido para diferenciar de los codigos autogenerados
        If query?.Any() Then
            Threading.Tasks.Parallel.ForEach(query, Sub(x)
                                                        x.CodeTmp = x.Codigo
                                                    End Sub)
        End If
        Return query
    End Function

End Class