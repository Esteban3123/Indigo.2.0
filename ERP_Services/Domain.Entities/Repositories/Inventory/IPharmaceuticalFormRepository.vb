'************************************************************
' Assembly         : Domain.Inventory.IGroupRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 22/09/2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region


Public Interface IPharmaceuticalFormRepository
    Inherits IRepository(Of PharmaceuticalForm)

    ''' <summary>
    ''' Obtiene una forma farmaceutica por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPharmaceuticalForm(code As String) As PharmaceuticalForm

    ''' <summary>
    ''' Obtiene una forma farmaceutica por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPharmaceuticalFormById(id As Integer) As PharmaceuticalForm

    ''' <summary>
    ''' Se utiliza para Almacenar la Forma Farmacéutica
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Function SavePharmaceuticalForm(xml As String, CodeUser As String) As SP_SavePharmaceuticalForm_Result

    ''' <summary>
    ''' Elimina por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Function SP_DeletePharmaceuticalForm(Id As Integer) As SP_DeletePharmaceuticalForm_Result

End Interface
