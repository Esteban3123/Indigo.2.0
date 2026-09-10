Imports Domain.Entities
Public Class AddGroupersCareGroup
    Inherits EventArgs

    ''' <summary>
    ''' Detalle equipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property GrouperCareGroup As GroupersCareGroup


    ''' <summary>
    ''' Listado de libros oficiales
    ''' </summary>
    ''' <remarks></remarks>
    Property ListDeleteGroupersCareGroupCups As List(Of GroupersCareGroupCups)

    ''' <summary>
    ''' Listado de libros oficiales
    ''' </summary>
    ''' <remarks></remarks>
    Property ListDeleteGroupersCareGroupActivities As List(Of GroupersCareGroupActivities)

    ''' <summary>
    ''' Listado que se devuelve al form principal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListGrouperCareGroup As List(Of GroupersCareGroup)
    Property GrouperCareGroupList As List(Of GroupersCareGroup)
End Class
