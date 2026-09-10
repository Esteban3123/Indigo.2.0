'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Hector Rodriguez Rubiano
' Created          : 08-01-2020
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports DevExpress.Xpo
Imports Presentation.Base
#End Region

Public Interface IInventorySupplie
    Inherits IcrudBase

#Region "properties"
    ''' <summary>
    ''' Esta propiedad contiene el codigo del insumo
    ''' </summary>
    Property Code As String
    ''' <summary>
    ''' Esta propiedad contiene el nombre del insumo
    ''' </summary>
    Property SupplieName As String
    ''' <summary>
    ''' Esta propiedad contiene el id de nivel de riesgo
    ''' </summary>
    ''' <returns></returns>
    Property RiskLevelId As Integer?
    ''' <summary>
    ''' Esta propiedad contiene si el insumo esta incluido en el plan de beneficios de salud
    ''' </summary>
    ''' <returns></returns>
    Property PBSProduct As Boolean?

    ''' <summary>
    ''' Esta propiedad contiene si exige justificacion de insumos/dispositivos
    ''' </summary>
    ''' <returns></returns>
    Property JustificationOfInputs As Boolean?

    ''' <summary>
    ''' Esta propiedad contiene si el insumo es Material Osteosíntesis
    ''' </summary>
    ''' <returns></returns>
    Property OsteosynthesisMaterial As Boolean?

    ''' <summary>
    ''' Esta propiedad contiene si el Insumo es De Consumo
    ''' </summary>
    ''' <returns></returns>
    Property Consumption As Boolean?

    ''' <summary>
    ''' Esta propiedad contiene si el Insumo es un Dispositivo de optometría
    ''' </summary>
    ''' <returns></returns>
    Property OptometryDevice As Boolean?

    ''' <summary>
    ''' Esta propiedad contiene si el Insumo es un Dispositivo Medico
    ''' </summary>
    ''' <returns></returns>
    Property MedicalDevice As Boolean?

    ''' <summary>
    ''' Esta propiedad contiene si el insumo se usa en nutrición parenteral 
    ''' </summary>
    ''' <returns></returns>
    Property IsParenteralNutritionSupply As Boolean?

    ''' <summary>
    ''' Esta Propiedad contiene el Estado del insumo
    ''' </summary>
    Property Status As Boolean

    ''' <summary>
    ''' Establece el datasource de niveles de riesgo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InventoryRiskLevel As XPInstantFeedbackSource

    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.InventorySequence

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

#End Region

End Interface

