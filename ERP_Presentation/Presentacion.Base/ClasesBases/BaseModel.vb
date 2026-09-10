'***********************************************************************
' Assembly         : Presentacion.Base
' Author           : Jorge Leonardo Vernaza
' Created          : 12-10-2011
'
' Last Modified By : Jorge Leonardo Vernaza
' Last Modified On : 12-10-2011
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias Importadas"
Imports Microsoft.Practices.EnterpriseLibrary.Common.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Validation
#End Region

''' <summary>
''' Clase Modelo Base que se hereda en todos los Modelos de los frontales para hacer validaciones con Enterprise Library
''' </summary>
''' <typeparam name="Entity"></typeparam>
Public MustInherit Class ModeloBase(Of Entity)

    ''' <summary>
    ''' Variable que se utiliza para validar el obejto
    ''' </summary>
    Private ValidarObjeto As Validator(Of Entity)


    Private _Controles As New List(Of ControlsValidations)
    ''' <summary>
    ''' propiedad que obtiene y envia el Nombre de los controles que no pasan la validacion de EntLib - Validating
    ''' </summary>
    ''' <value>el nombre del control.</value>
    Property Controles As List(Of ControlsValidations)
        Get
            Return _Controles
        End Get
        Set(ByVal value As List(Of ControlsValidations))
            _Controles = value
        End Set
    End Property

    ''' <summary>
    ''' Funcion sobre escribible para validar el objeto con enterprise library antes de guardar
    ''' </summary>
    ''' <param name="objeto">objeto.</param>
    ''' <returns>Un mensaje en caso de que halla incumplido alguna validacion</returns>
    Overridable Function Guardar(ByVal objeto As Entity) As List(Of ControlsValidations)
        'Dim Validacion As ValidatorFactory = IoCFactory.Instance.CurrentContainer.Resolve(Of ValidatorFactory)()
        Dim Resultado As ValidationResults
        Dim Validacion As ValidatorFactory = EnterpriseLibraryContainer.Current.GetInstance(Of ValidatorFactory)()
        ValidarObjeto = Validacion.CreateValidator(Of Entity)()
        Resultado = ValidarObjeto.Validate(objeto)
        For Each result In Resultado
            _Controles.Add(New ControlsValidations With {.Tag = result.Tag, .Mensaje = result.Message})
        Next
        Return Nothing
    End Function

End Class


