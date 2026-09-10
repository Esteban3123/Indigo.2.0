'***********************************************************************
' Assembly         : DistributedServices.MixingStation
' Author           : Yoe Andres Cardenas
' Created          : 25/04/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
#End Region

<ServiceContract()>
Public Interface IMixingStationServiceSequence

#Region "Methods"

    ''' <summary>
    ''' Obtiene la secuencia numerica para un formulario
    ''' </summary>
    ''' <param name="idForm">Id del formulario a consultar</param>
    ''' <returns>Secuencia numerica</returns>
    <OperationContract()>
    Function GetSequenceByIdForm(idForm As String) As MixingStationSequence

    ''' <summary>
    ''' Obtiene un grupo de secuencias numericas por su id de configuración
    ''' </summary>
    ''' <param name="id">Id de la configuración de la secuencia</param>
    ''' <returns>Grupo de secuencias numericas</returns>
    <OperationContract()>
    Function GetNumericSequenceGroupById(id As Integer) As List(Of String)

    <OperationContract()>
    Function SaveSequence(ByVal seq As MixingStationSequence) As ActionResult

#End Region

End Interface
