'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Judy Andrea Díaz Reyes
' Created          : 06-05-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface ICMConfigRepository
    Inherits IRepository(Of CMConfiguration)

    ''' <summary>
    ''' Obtiene todos los parametros de configuración de central de mezclas
    ''' </summary>
    ''' <returns>Lista de Monedas</returns>
    ''' <remarks></remarks>
    Function ListAllCMConfig() As List(Of CMConfiguration)

    ''' <summary>
    ''' Obtiene un tipo de dosis unitaria por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetCMConfig(code As String, Optional tracking As Boolean = True) As CMConfiguration

    ''' <summary>
    ''' Consulta unidades funcionales de centros de atencion de una central de mezcla
    ''' </summary>
    ''' <param name="Xml"></param>
    ''' <returns></returns>
    Function ListCMCenterLineUnit(Xml As String) As List(Of SP_CMCenterLineUnit_Result)

    ''' <summary>
    ''' Se valida que el medicamento con el tipo de dosis unitaria no exista
    ''' </summary>
    ''' <param name="MedicinesProduction"></param>
    ''' <returns></returns>
    Function ValidateMedicineProduction(MedicinesProduction As MedicinesProduction) As String

    ''' <summary>
    ''' Permite importar los medicamentos de producción
    ''' </summary>
    ''' <param name="xmlObject"></param>
    ''' <param name="CMConfigurationId"></param>
    ''' <returns></returns>
    Function SP_ImportMedicinesProduction(xmlObject As String, CMConfigurationId As Integer) As List(Of SP_ImportMedicinesProduction_Result)

    ''' <summary>
    ''' Obtener la Central de Mezcla por ID
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Function GetCMDetailById(id As Integer) As CMConfiguration

End Interface
