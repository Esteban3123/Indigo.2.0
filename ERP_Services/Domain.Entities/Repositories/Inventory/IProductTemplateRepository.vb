'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Diego Andrés Roldán Lozano
' Created          : 03-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Threading.Tasks
Imports Domain.Base
Imports Domain.Entities

Public Interface IProductTemplateRepository
    Inherits IRepository(Of ProductRate)

    ''' <summary>
    ''' Obtiene un cubrimiento por codigo
    ''' </summary>
    Function GetProductTemplate(ByVal code As String) As Task(Of ProductRate)

    ''' <summary>
    ''' Obtiene un cubrimiento por id
    ''' </summary>
    Function GetProductTemplateById(id As Integer) As ProductRate


    Function GetProductRateGeneralConditionByTemplateById(id As Integer) As List(Of ProductRateGeneral)

    ''' <summary>
    ''' Obtienelistado de todas las tarifas
    ''' </summary>
    Function GetProductTemplateAll() As List(Of ProductRate)

    Sub DeleteDetailUpdatedById(id As Integer)
    Sub DeleteConditionsById(id As Integer)
    Sub DeleteConditionsAllById(id As Integer)

End Interface