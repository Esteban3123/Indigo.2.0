'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Daniel Eduardo Arévalo
' Created          : 19-03-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Presentation.Base
Imports Presentation.CloudAgent.IndigoReference.Payroll
Imports Domain.Maintenance.Entities
#End Region

Public Interface IEquipmentTypePartsAccesoriesConsumibles

    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Esta propiedad contiene el Id del Tipo del Equipo
    ''' </summary>
    Property IdEquipmentType As Integer

    ''' <summary>
    ''' Esta Propiedad contiene el Id de Registro Tecnico
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdPartsAccesoriesConsumibles As Integer

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Obtiene los Tipos de Equipo
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property ListEquipmentType As List(Of EquipmentType)

    ''' <summary>
    ''' Obtiene los Resgistros Técnicos
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property ListPartsAccesoriesConsumibles As List(Of PartsAccesoriesConsumables)

#End Region


End Interface
