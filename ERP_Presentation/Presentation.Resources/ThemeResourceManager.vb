Imports System.Drawing
Imports DevExpress.Utils
Imports Infrastructure.CrossCutting.Base
Imports System.Windows.Forms
Imports DevExpress.LookAndFeel
Imports DevExpress.Skins
Imports DevExpress.XtraEditors
Imports DevExpress.XtraLayout
Imports DevExpress.Utils.Svg

Public NotInheritable Class ThemeResourceManager

#Region "Fields"

    ''' <summary>
    ''' Administrador de colecciones de iconos
    ''' </summary>
    Private Shared ReadOnly ImageCollectionManager As New ImageCollectionManager()

#End Region

#Region "Methods"
    Public Shared Sub ApplyStyleThemeToControl(ctr As Control, _enabled As Boolean)
        Try
            If TypeOf ctr Is ISupportLookAndFeel Then
                Dim currentSkin As DevExpress.Skins.Skin = DevExpress.Skins.CommonSkins.GetSkin(CType(ctr, ISupportLookAndFeel).LookAndFeel)
                If _enabled Then
                    ctr.BackColor = currentSkin.Colors.GetColor(DevExpress.Skins.CommonColors.Window)
                    If UserLookAndFeel.Default.ActiveLookAndFeel.ActiveSkinName = "Darkroom" Then
                        ctr.ForeColor = Color.White
                    ElseIf UserLookAndFeel.Default.ActiveLookAndFeel.ActiveSkinName = "Sharp Plus" Then
                        ctr.ForeColor = Color.Black
                    Else
                        ctr.ForeColor = currentSkin.Colors.GetColor(DevExpress.Skins.CommonColors.WindowText)
                    End If
                Else
                    ctr.BackColor = currentSkin.Colors.GetColor(DevExpress.Skins.CommonColors.DisabledControl)
                    If UserLookAndFeel.Default.ActiveLookAndFeel.ActiveSkinName = "Darkroom" Then
                        ctr.ForeColor = Color.WhiteSmoke
                    ElseIf UserLookAndFeel.Default.ActiveLookAndFeel.ActiveSkinName = "Sharp Plus" Then
                        ctr.ForeColor = Color.DarkGray
                    Else
                        ctr.ForeColor = currentSkin.Colors.GetColor(DevExpress.Skins.CommonColors.DisabledText)
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Shared Sub ApplyStyleThemeToControl(ctr As Control)
        Try

            If ctr.Parent IsNot Nothing Then
                ctr.Parent.BackColor = Color.Transparent
            End If
            ctr.BackColor = Color.Transparent

            If TypeOf ctr Is ISupportLookAndFeel Then
                Dim currentSkin As DevExpress.Skins.Skin = DevExpress.Skins.CommonSkins.GetSkin(CType(ctr, ISupportLookAndFeel).LookAndFeel)
                If UserLookAndFeel.Default.ActiveLookAndFeel.ActiveSkinName = "Darkroom" Then
                    ctr.ForeColor = Color.White
                ElseIf UserLookAndFeel.Default.ActiveLookAndFeel.ActiveSkinName = "Sharp Plus" Then
                    ctr.ForeColor = Color.Black
                Else
                    ctr.ForeColor = currentSkin.Colors.GetColor(DevExpress.Skins.CommonColors.WindowText)
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Public Shared Sub ApplyStyleThemeToControl(ctr As LayoutControlItem)

        If ctr.AppearanceItemCaption IsNot Nothing Then
            ctr.AppearanceItemCaption.BackColor = Color.Transparent
            ctr.AppearanceItemCaption.ForeColor = DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinMaskColor
        End If
    End Sub

    ''' <summary>
    ''' Asigna estilo al control y controles internos
    ''' </summary>
    ''' <param name="ctr">Control a aplicar estilo</param>
    Public Shared Sub SetStyleThemeOnControl(ByVal ctr As Control)

        Dim colr As Color = UserLookAndFeel.Default.SkinMaskColor
        If colr <> Color.Transparent Then
            If TypeOf ctr Is XtraForm Then
                CType(ctr, XtraForm).Appearance.BackColor = Color.Transparent
                CType(ctr, XtraForm).Appearance.BackColor2 = Color.Transparent
                CType(ctr, XtraForm).Appearance.ForeColor = colr

            ElseIf TypeOf ctr Is XtraUserControl Then
                CType(ctr, XtraUserControl).Appearance.BackColor = Color.Transparent
                CType(ctr, XtraUserControl).Appearance.BackColor2 = Color.Transparent
                CType(ctr, XtraUserControl).Appearance.ForeColor = colr
            End If
        End If

        For Each c As Control In ctr.Controls
            If TypeOf c Is LabelControl Then
                'CType(c, LabelControl).ResetForeColor()
                ApplyStyleThemeToControl(c)
            ElseIf TypeOf c Is PopupContainerEdit Then
                'CType(c, PopupContainerEdit).ResetForeColor()
                ApplyStyleThemeToControl(c)
                SetStyleThemeOnControl(c)
            ElseIf TypeOf c Is SimpleButton Then
                CType(c, SimpleButton).ResetBackColor()
                CType(c, SimpleButton).BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default
                CType(c, SimpleButton).Appearance.BorderColor = Color.Empty
                CType(c, SimpleButton).ResetForeColor()
                CType(c, SimpleButton).LookAndFeel.Style = ActiveLookAndFeelStyle.Skin
                SetStyleThemeOnControl(c)
            Else
                SetStyleThemeOnControl(c)
            End If

        Next
        'End If
    End Sub

    ''' <summary>
    ''' Obtiene el icono para el mensaje de error según el tema
    ''' </summary>
    ''' <param name="theme">Nombre del tema a usar</param>
    ''' <returns>Icono</returns>
    Public Shared Function GetIconMessageErrorIndigo(ByVal theme As String) As Image
        Dim img As Image = Nothing
        Select Case theme
            'Blancos
            Case "Sharp Plus", "Office 2007 Black", "Black", "Blueprint", "Dakroom", "Dark Side", "Dark Style", "Metropolis Dark", "Office 2010 Black", "Pumpkim", "Visual Studio 2013 Dark"
                img = ImageCollectionManager.ImageIconMessageErrorIndigo.Images("Blanco.png")
                'Cafe
            Case "Caramel", "Coffee"
                img = ImageCollectionManager.ImageIconMessageErrorIndigo.Images("Cafe.png")
                'Fucsia
            Case "Office 2007 Pink", "Valentine"
                img = ImageCollectionManager.ImageIconMessageErrorIndigo.Images("Fucsia.png")
                'Lila
            Case "The Bezier", "High Contrast", "Seven", "Blue", "Devexpress Style", "Foggy", "Lilian", "Liquid Sky", "London Liquid Sky", "Money Twins", "Office 2007 Blue", "Office 2007 Silver", "Office 2010 Blue", "Office 2013", "Office 2013 Dark Gray", "Office 2013 Light Gray", "Seven Classic", "Stardust", "The Asphalt World", "VS2010", "Whiteprint", "Xmas (Blue)", "Visual Studio 2013 Light"
                img = ImageCollectionManager.ImageIconMessageErrorIndigo.Images("Lila.png")
                'Verde
            Case "Springtime", "Glass Oceans", "iMaginary", "Metropolis", "Office 2007 Green", "Office 2010 Silver", "Summer"
                img = ImageCollectionManager.ImageIconMessageErrorIndigo.Images("Verde.png")
            Case Else 'Azul
                'img = ImageCollectionManager.ImageIconMessageErrorIndigo.Images("Blanco.png")
                If (Not theme.Equals("Office 2013 Dark Gray")) AndAlso (theme.Contains("Black") OrElse theme.Contains("Dark") OrElse theme.Contains("Oscuro") OrElse theme.Contains("Negro") OrElse theme.Contains("Sharp") OrElse theme.Contains("Pumpkin")) Then
                    img = ImageCollectionManager.ImageIconMessageErrorIndigo.Images("Blanco.png")
                Else
                    img = ImageCollectionManager.ImageIconMessageErrorIndigo.Images("Lila.png")
                End If
        End Select
        Return img
    End Function

    ''' <summary>
    ''' Obtiene el icono para el mensaje según el tema
    ''' </summary>
    ''' <param name="theme">Nombre del tema a usar</param>
    ''' <param name="mType">Tipo de icono</param>
    ''' <param name="zoomImage">Ajusta el tamaño de la imagen a mostrar</param>
    ''' <returns>Icono</returns>
    Public Shared Function GetIconMessageIndigo(ByVal theme As String, ByVal mType As MessageType, ByVal zoomImage As Boolean) As Image
        Dim img As Image = Nothing
        Select Case theme
            'Blancos
            Case "Sharp Plus", "Office 2007 Black", "Black", "Blueprint", "Dakroom", "Dark Side", "Dark Style", "Metropolis Dark", "Office 2010 Black", "Pumpkim", "Visual Studio 2013 Dark", "The Bezier", "High Contrast"
                Select Case mType
                    Case MessageType.Information
                        img = ImageCollectionManager.ImageIconMessageIndigo.Images("IBlanco.png")
                    Case MessageType.Errores
                        img = ImageCollectionManager.ImageIconMessageIndigo.Images("EBlanco.png")
                    Case MessageType.Warning
                        img = ImageCollectionManager.ImageIconMessageIndigo.Images("ABlanco.png")
                    Case MessageType.Question
                        img = ImageCollectionManager.ImageIconMessageIndigo.Images("PBlanco.png")
                    Case Else
                        img = ImageCollectionManager.ImageIconMessageIndigo.Images("EBlanco.png")
                End Select
                'Cafe
            Case "Caramel", "Coffee"
                Select Case mType
                    Case MessageType.Information
                        img = ImageCollectionManager.ImageIconMessageIndigo.Images("ICafe.png")
                    Case MessageType.Errores
                        img = ImageCollectionManager.ImageIconMessageIndigo.Images("ECafe.png")
                    Case MessageType.Warning
                        img = ImageCollectionManager.ImageIconMessageIndigo.Images("ACafe.png")
                    Case MessageType.Question
                        img = ImageCollectionManager.ImageIconMessageIndigo.Images("PCafe.png")
                    Case Else
                        img = ImageCollectionManager.ImageIconMessageIndigo.Images("ECafe.png")
                End Select
                'Fucsia
            Case "Office 2007 Pink", "Valentine"
                Select Case mType
                    Case MessageType.Information
                        img = ImageCollectionManager.ImageIconMessageIndigo.Images("IFucsia.png")
                    Case MessageType.Errores
                        img = ImageCollectionManager.ImageIconMessageIndigo.Images("EFucsia.png")
                    Case MessageType.Warning
                        img = ImageCollectionManager.ImageIconMessageIndigo.Images("AFucsia.png")
                    Case MessageType.Question
                        img = ImageCollectionManager.ImageIconMessageIndigo.Images("PFucsia.png")
                    Case Else
                        img = ImageCollectionManager.ImageIconMessageIndigo.Images("EFucsia.png")
                End Select
                'Lila
            Case "Seven", "Blue", "Devexpress Style", "Foggy", "Lilian", "Liquid Sky", "London Liquid Sky", "Money Twins", "Office 2007 Blue", "Office 2007 Silver", "Office 2010 Blue", "Office 2013", "Office 2013 Dark Gray", "Office 2013 Light Gray", "Seven Classic", "Stardust", "The Asphalt World", "VS2010", "Whiteprint", "Xmas (Blue)", "Visual Studio 2013 Light"
                Select Case mType
                    Case MessageType.Information
                        img = ImageCollectionManager.ImageIconMessageIndigo.Images("ILila.png")
                    Case MessageType.Errores
                        img = ImageCollectionManager.ImageIconMessageIndigo.Images("ELila.png")
                    Case MessageType.Warning
                        img = ImageCollectionManager.ImageIconMessageIndigo.Images("ALila.png")
                    Case MessageType.Question
                        img = ImageCollectionManager.ImageIconMessageIndigo.Images("PLila.png")
                    Case Else
                        img = ImageCollectionManager.ImageIconMessageIndigo.Images("ELila.png")
                End Select
                'Verde
            Case "Springtime", "Glass Oceans", "iMaginary", "Metropolis", "Office 2007 Green", "Office 2010 Silver", "Summer"
                Select Case mType
                    Case MessageType.Information
                        img = ImageCollectionManager.ImageIconMessageIndigo.Images("IVerde.png")
                    Case MessageType.Errores
                        img = ImageCollectionManager.ImageIconMessageIndigo.Images("EVerde.png")
                    Case MessageType.Warning
                        img = ImageCollectionManager.ImageIconMessageIndigo.Images("AVerde.png")
                    Case MessageType.Question
                        img = ImageCollectionManager.ImageIconMessageIndigo.Images("PVerde.png")
                    Case Else
                        img = ImageCollectionManager.ImageIconMessageIndigo.Images("EVerde.png")
                End Select
            Case Else 'Azul

                If (Not theme.Equals("Office 2013 Dark Gray")) AndAlso (theme.Contains("Black") OrElse theme.Contains("Dark") OrElse theme.Contains("Oscuro") OrElse theme.Contains("Negro") OrElse theme.Contains("Sharp") OrElse theme.Contains("Pumpkin")) Then
                    Select Case mType
                        Case MessageType.Information
                            img = ImageCollectionManager.ImageIconMessageIndigo.Images("IBlanco.png")
                        Case MessageType.Errores
                            img = ImageCollectionManager.ImageIconMessageIndigo.Images("EBlanco.png")
                        Case MessageType.Warning
                            img = ImageCollectionManager.ImageIconMessageIndigo.Images("ABlanco.png")
                        Case MessageType.Question
                            img = ImageCollectionManager.ImageIconMessageIndigo.Images("PBlanco.png")
                        Case Else
                            img = ImageCollectionManager.ImageIconMessageIndigo.Images("EBlanco.png")
                    End Select
                Else
                    Select Case mType
                        Case MessageType.Information
                            img = ImageCollectionManager.ImageIconMessageIndigo.Images("ILila.png")
                        Case MessageType.Errores
                            img = ImageCollectionManager.ImageIconMessageIndigo.Images("ELila.png")
                        Case MessageType.Warning
                            img = ImageCollectionManager.ImageIconMessageIndigo.Images("ALila.png")
                        Case MessageType.Question
                            img = ImageCollectionManager.ImageIconMessageIndigo.Images("PLila.png")
                        Case Else
                            img = ImageCollectionManager.ImageIconMessageIndigo.Images("ELila.png")
                    End Select
                End If
        End Select
        'Return img
        If zoomImage Then
            Return New Bitmap(img, New Size(90, 90))
        Else
            Return New Bitmap(img, New Size(64, 64))
        End If

    End Function

    ''' <summary>
    ''' Obtiene la colección de imagenes para la barra de botones
    ''' </summary>
    ''' <param name="theme">Nombre del tema a usar</param>
    ''' <param name="isLarge">Valor que indica si se requiere iconos grandes</param>
    ''' <returns>Colección de imagenes a usar</returns>
    Public Shared Function GetImageCollectionToToolbarByTheme(ByVal theme As String, Optional ByVal isLarge As Boolean = False) As ImageCollection
        Select Case theme
            'Blancos
            Case "Sharp Plus", "Office 2007 Black", "Black", "Blueprint", "Dakroom", "Dark Side", "Dark Style", "Metropolis Dark", "Office 2010 Black", "Pumpkim", "Visual Studio 2013 Dark"
                If isLarge Then
                    Return ImageCollectionManager.ToolBarIconsWhiteLarge
                Else
                    Return ImageCollectionManager.ToolBarIconsWhiteSmall
                End If
                'Cafe
            Case "Caramel", "Coffee"
                If isLarge Then
                    Return ImageCollectionManager.ToolBarIconsBrownLarge
                Else
                    Return ImageCollectionManager.ToolBarIconsBrownSmall
                End If
                'Fucsia
            Case "Office 2007 Pink", "Valentine"
                If isLarge Then
                    Return ImageCollectionManager.ToolBarIconsFucsiaLarge
                Else
                    Return ImageCollectionManager.ToolBarIconsFucsiaSmall
                End If
                'Lila
            Case "The Bezier", "High Contrast", "Seven", "Blue", "Devexpress Style", "Foggy", "Lilian", "Liquid Sky", "London Liquid Sky", "Money Twins", "Office 2007 Blue", "Office 2007 Silver", "Office 2010 Blue", "Office 2013", "Office 2013 Dark Gray", "Office 2013 Light Gray", "Seven Classic", "Sharp", "Stardust", "The Asphalt World", "VS2010", "Whiteprint", "Xmas (Blue)", "Visual Studio 2013 Light"
                If isLarge Then
                    Return ImageCollectionManager.ToolBarIconsLilaLarge
                Else
                    Return ImageCollectionManager.ToolBarIconsLilaSmall
                End If
                'Verde
            Case "Springtime", "Glass Oceans", "iMaginary", "Metropolis", "Office 2007 Green", "Office 2010 Silver", "Summer"
                If isLarge Then
                    Return ImageCollectionManager.ToolBarIconsGreenLarge
                Else
                    Return ImageCollectionManager.ToolBarIconsGreenSmall
                End If
            Case Else 'Azul o cualquier otro

                If (Not theme.Equals("Office 2013 Dark Gray")) AndAlso (theme.Contains("Black") OrElse theme.Contains("Dark") OrElse theme.Contains("Oscuro") OrElse theme.Contains("Negro") OrElse theme.Contains("Sharp") OrElse theme.Contains("Pumpkin")) Then
                    If isLarge Then
                        Return ImageCollectionManager.ToolBarIconsWhiteLarge
                    Else
                        Return ImageCollectionManager.ToolBarIconsWhiteSmall
                    End If
                Else
                    If isLarge Then
                        Return ImageCollectionManager.ToolBarIconsLilaLarge
                    Else
                        Return ImageCollectionManager.ToolBarIconsLilaSmall
                    End If
                End If
        End Select
    End Function

    ''' <summary>
    ''' Obtiene la imagen del boton de salir de aplicación
    ''' </summary>
    ''' <param name="theme">Nombre del tema a usar</param>
    ''' <returns>Imagen a usar</returns>
    Public Shared Function GetUnlockUserButtonImageByTheme(ByVal theme As String) As Image
        Select Case theme
            'Blancos
            Case "Sharp Plus", "Office 2007 Black", "Black", "Blueprint", "Dakroom", "Dark Side", "Dark Style", "Metropolis Dark", "Office 2010 Black", "Pumpkim", "Visual Studio 2013 Dark"
                Return ImageCollectionManager.ImageIconsOtherWhite.Images(7)
                'Cafe
            Case "Caramel", "Coffee"
                Return ImageCollectionManager.ImageIconsOtherBrown.Images(7)
                'Fucsia
            Case "Office 2007 Pink", "Valentine"
                Return ImageCollectionManager.ImageIconsOtherFucsia.Images(7)
                'Lila
            Case "The Bezier", "High Contrast", "Seven", "Blue", "Devexpress Style", "Foggy", "Lilian", "Liquid Sky", "London Liquid Sky", "Money Twins", "Office 2007 Blue", "Office 2007 Silver", "Office 2010 Blue", "Office 2013", "Office 2013 Dark Gray", "Office 2013 Light Gray", "Seven Classic", "Stardust", "The Asphalt World", "VS2010", "Whiteprint", "Xmas (Blue)", "Visual Studio 2013 Light"
                Return ImageCollectionManager.ImageIconsOtherLila.Images(7)
                'Verde
            Case "Springtime", "Glass Oceans", "iMaginary", "Metropolis", "Office 2007 Green", "Office 2010 Silver", "Summer"
                Return ImageCollectionManager.ImageIconsOtherGreen.Images(7)
            Case Else 'Azul
                If (Not theme.Equals("Office 2013 Dark Gray")) AndAlso (theme.Contains("Black") OrElse theme.Contains("Dark") OrElse theme.Contains("Oscuro") OrElse theme.Contains("Negro") OrElse theme.Contains("Sharp") OrElse theme.Contains("Pumpkin")) Then
                    Return ImageCollectionManager.ImageIconsOtherWhite.Images(7)
                Else
                    Return ImageCollectionManager.ImageIconsOtherLila.Images(7)
                End If
        End Select
    End Function

    ''' <summary>
    ''' Obtiene la imagen del boton de salir de aplicación
    ''' </summary>
    ''' <param name="theme">Nombre del tema a usar</param>
    ''' <returns>Imagen a usar</returns>
    Public Shared Function GetGroupsButtonImageByTheme(ByVal theme As String) As Image
        Select Case theme
            'Blancos
            Case "Sharp Plus", "Office 2007 Black", "Black", "Blueprint", "Dakroom", "Dark Side", "Dark Style", "Metropolis Dark", "Office 2010 Black", "Pumpkim", "Visual Studio 2013 Dark"
                Return ImageCollectionManager.ImageIconsOtherWhite.Images(1)
                'Cafe
            Case "Caramel", "Coffee"
                Return ImageCollectionManager.ImageIconsOtherBrown.Images(1)
                'Fucsia
            Case "Office 2007 Pink", "Valentine"
                Return ImageCollectionManager.ImageIconsOtherFucsia.Images(1)
                'Lila
            Case "The Bezier", "High Contrast", "Seven", "Blue", "Devexpress Style", "Foggy", "Lilian", "Liquid Sky", "London Liquid Sky", "Money Twins", "Office 2007 Blue", "Office 2007 Silver", "Office 2010 Blue", "Office 2013", "Office 2013 Dark Gray", "Office 2013 Light Gray", "Seven Classic", "Stardust", "The Asphalt World", "VS2010", "Whiteprint", "Xmas (Blue)", "Visual Studio 2013 Light"
                Return ImageCollectionManager.ImageIconsOtherLila.Images(1)
                'Verde
            Case "Springtime", "Glass Oceans", "iMaginary", "Metropolis", "Office 2007 Green", "Office 2010 Silver", "Summer"
                Return ImageCollectionManager.ImageIconsOtherGreen.Images(1)
            Case Else 'Azul
                If (Not theme.Equals("Office 2013 Dark Gray")) AndAlso (theme.Contains("Black") OrElse theme.Contains("Dark") OrElse theme.Contains("Oscuro") OrElse theme.Contains("Negro") OrElse theme.Contains("Sharp") OrElse theme.Contains("Pumpkin")) Then
                    Return ImageCollectionManager.ImageIconsOtherWhite.Images(1)
                Else
                    Return ImageCollectionManager.ImageIconsOtherLila.Images(1)
                End If
        End Select
    End Function

    ''' <summary>
    ''' Obtiene la imagen del boton de salir de aplicación
    ''' </summary>
    ''' <param name="theme">Nombre del tema a usar</param>
    ''' <returns>Imagen a usar</returns>
    Public Shared Function GetRolesButtonImageByTheme(ByVal theme As String) As Image
        Select Case theme
            'Blancos
            Case "Sharp Plus", "Office 2007 Black", "Black", "Blueprint", "Dakroom", "Dark Side", "Dark Style", "Metropolis Dark", "Office 2010 Black", "Pumpkim", "Visual Studio 2013 Dark"
                Return ImageCollectionManager.ImageIconsOtherWhite.Images(1)
                'Cafe
            Case "Caramel", "Coffee"
                Return ImageCollectionManager.ImageIconsOtherBrown.Images(1)
                'Fucsia
            Case "Office 2007 Pink", "Valentine"
                Return ImageCollectionManager.ImageIconsOtherFucsia.Images(1)
                'Lila
            Case "The Bezier", "High Contrast", "Seven", "Blue", "Devexpress Style", "Foggy", "Lilian", "Liquid Sky", "London Liquid Sky", "Money Twins", "Office 2007 Blue", "Office 2007 Silver", "Office 2010 Blue", "Office 2013", "Office 2013 Dark Gray", "Office 2013 Light Gray", "Seven Classic", "Stardust", "The Asphalt World", "VS2010", "Whiteprint", "Xmas (Blue)", "Visual Studio 2013 Light"
                Return ImageCollectionManager.ImageIconsOtherLila.Images(1)
                'Verde
            Case "Springtime", "Glass Oceans", "iMaginary", "Metropolis", "Office 2007 Green", "Office 2010 Silver", "Summer"
                Return ImageCollectionManager.ImageIconsOtherGreen.Images(1)
            Case Else 'Azul
                If (Not theme.Equals("Office 2013 Dark Gray")) AndAlso (theme.Contains("Black") OrElse theme.Contains("Dark") OrElse theme.Contains("Oscuro") OrElse theme.Contains("Negro") OrElse theme.Contains("Sharp") OrElse theme.Contains("Pumpkin")) Then
                    Return ImageCollectionManager.ImageIconsOtherWhite.Images(1)
                Else
                    Return ImageCollectionManager.ImageIconsOtherLila.Images(1)
                End If
        End Select
    End Function

    ''' <summary>
    ''' Obtiene la imagen del boton de salir de aplicación
    ''' </summary>
    ''' <param name="theme">Nombre del tema a usar</param>
    ''' <returns>Imagen a usar</returns>
    Public Shared Function GetUsersButtonImageByTheme(ByVal theme As String) As Image
        Select Case theme
            'Blancos
            Case "Sharp Plus", "Office 2007 Black", "Black", "Blueprint", "Dakroom", "Dark Side", "Dark Style", "Metropolis Dark", "Office 2010 Black", "Pumpkim", "Visual Studio 2013 Dark"
                Return ImageCollectionManager.ImageIconsOtherWhite.Images(5)
                'Cafe
            Case "Caramel", "Coffee"
                Return ImageCollectionManager.ImageIconsOtherBrown.Images(5)
                'Fucsia
            Case "Office 2007 Pink", "Valentine"
                Return ImageCollectionManager.ImageIconsOtherFucsia.Images(5)
                'Lila
            Case "The Bezier", "High Contrast", "Seven", "Blue", "Devexpress Style", "Foggy", "Lilian", "Liquid Sky", "London Liquid Sky", "Money Twins", "Office 2007 Blue", "Office 2007 Silver", "Office 2010 Blue", "Office 2013", "Office 2013 Dark Gray", "Office 2013 Light Gray", "Seven Classic", "Stardust", "The Asphalt World", "VS2010", "Whiteprint", "Xmas (Blue)", "Visual Studio 2013 Light"
                Return ImageCollectionManager.ImageIconsOtherLila.Images(5)
                'Verde
            Case "Springtime", "Glass Oceans", "iMaginary", "Metropolis", "Office 2007 Green", "Office 2010 Silver", "Summer"
                Return ImageCollectionManager.ImageIconsOtherGreen.Images(5)
            Case Else 'Azul
                If (Not theme.Equals("Office 2013 Dark Gray")) AndAlso (theme.Contains("Black") OrElse theme.Contains("Dark") OrElse theme.Contains("Oscuro") OrElse theme.Contains("Negro") OrElse theme.Contains("Sharp") OrElse theme.Contains("Pumpkin")) Then
                    Return ImageCollectionManager.ImageIconsOtherWhite.Images(5)
                Else
                    Return ImageCollectionManager.ImageIconsOtherLila.Images(5)
                End If
        End Select
    End Function

    ''' <summary>
    ''' Obtiene la imagen del boton de salir de aplicación
    ''' </summary>
    ''' <param name="theme">Nombre del tema a usar</param>
    ''' <returns>Imagen a usar</returns>
    Public Shared Function GetConnectionButtonImageByTheme(ByVal theme As String) As Image
        Select Case theme
            'Blancos
            Case "Sharp Plus", "Office 2007 Black", "Black", "Blueprint", "Dakroom", "Dark Side", "Dark Style", "Metropolis Dark", "Office 2010 Black", "Pumpkim", "Visual Studio 2013 Dark"
                Return ImageCollectionManager.ImageIconsOtherWhite.Images(0)
                'Cafe
            Case "Caramel", "Coffee"
                Return ImageCollectionManager.ImageIconsOtherBrown.Images(0)
                'Fucsia
            Case "Office 2007 Pink", "Valentine"
                Return ImageCollectionManager.ImageIconsOtherFucsia.Images(0)
                'Lila
            Case "The Bezier", "High Contrast", "Seven", "Blue", "Devexpress Style", "Foggy", "Lilian", "Liquid Sky", "London Liquid Sky", "Money Twins", "Office 2007 Blue", "Office 2007 Silver", "Office 2010 Blue", "Office 2013", "Office 2013 Dark Gray", "Office 2013 Light Gray", "Seven Classic", "Stardust", "The Asphalt World", "VS2010", "Whiteprint", "Xmas (Blue)", "Visual Studio 2013 Light"
                Return ImageCollectionManager.ImageIconsOtherLila.Images(0)
                'Verde
            Case "Springtime", "Glass Oceans", "iMaginary", "Metropolis", "Office 2007 Green", "Office 2010 Silver", "Summer"
                Return ImageCollectionManager.ImageIconsOtherGreen.Images(0)
            Case Else 'Azul
                If (Not theme.Equals("Office 2013 Dark Gray")) AndAlso (theme.Contains("Black") OrElse theme.Contains("Dark") OrElse theme.Contains("Oscuro") OrElse theme.Contains("Negro") OrElse theme.Contains("Sharp") OrElse theme.Contains("Pumpkin")) Then
                    Return ImageCollectionManager.ImageIconsOtherWhite.Images(0)
                Else
                    Return ImageCollectionManager.ImageIconsOtherLila.Images(0)
                End If
        End Select
    End Function

    ''' <summary>
    ''' Obtiene la imagen del boton de salir de aplicación
    ''' </summary>
    ''' <param name="theme">Nombre del tema a usar</param>
    ''' <returns>Imagen a usar</returns>
    Public Shared Function GetExitButtonImageByTheme(ByVal theme As String) As Image
        Select Case theme
            'Blancos
            Case "Sharp Plus", "Office 2007 Black", "Black", "Blueprint", "Dakroom", "Dark Side", "Dark Style", "Metropolis Dark", "Office 2010 Black", "Pumpkim", "Visual Studio 2013 Dark"
                Return ImageCollectionManager.ImageIconsOtherWhite.Images(3)
                'Cafe
            Case "Caramel", "Coffee"
                Return ImageCollectionManager.ImageIconsOtherBrown.Images(3)
                'Fucsia
            Case "Office 2007 Pink", "Valentine"
                Return ImageCollectionManager.ImageIconsOtherFucsia.Images(3)
                'Lila
            Case "The Bezier", "High Contrast", "Seven", "Blue", "Devexpress Style", "Foggy", "Lilian", "Liquid Sky", "London Liquid Sky", "Money Twins", "Office 2007 Blue", "Office 2007 Silver", "Office 2010 Blue", "Office 2013", "Office 2013 Dark Gray", "Office 2013 Light Gray", "Seven Classic", "Stardust", "The Asphalt World", "VS2010", "Whiteprint", "Xmas (Blue)", "Visual Studio 2013 Light"
                Return ImageCollectionManager.ImageIconsOtherLila.Images(3)
                'Verde
            Case "Springtime", "Glass Oceans", "iMaginary", "Metropolis", "Office 2007 Green", "Office 2010 Silver", "Summer"
                Return ImageCollectionManager.ImageIconsOtherGreen.Images(3)
            Case Else 'Azul
                If (Not theme.Equals("Office 2013 Dark Gray")) AndAlso (theme.Contains("Black") OrElse theme.Contains("Dark") OrElse theme.Contains("Oscuro") OrElse theme.Contains("Negro") OrElse theme.Contains("Sharp") OrElse theme.Contains("Pumpkin")) Then
                    Return ImageCollectionManager.ImageIconsOtherWhite.Images(3)
                Else
                    Return ImageCollectionManager.ImageIconsOtherLila.Images(3)
                End If
        End Select
    End Function

    ''' <summary>
    ''' Obtiene la imagen del boton de cerrar sesión
    ''' </summary>
    ''' <param name="theme">Nombre del tema a usar</param>
    ''' <returns>Imagen a usar</returns>
    Public Shared Function GetCloseSessionButtonImageByTheme(ByVal theme As String) As Image
        Select Case theme
            'Blancos
            Case "Sharp Plus", "Office 2007 Black", "Black", "Blueprint", "Dakroom", "Dark Side", "Dark Style", "Metropolis Dark", "Office 2010 Black", "Pumpkim", "Visual Studio 2013 Dark"
                Return ImageCollectionManager.ImageIconsOtherWhite.Images(8)
                'Cafe
            Case "Caramel", "Coffee"
                Return ImageCollectionManager.ImageIconsOtherBrown.Images(8)
                'Fucsia
            Case "Office 2007 Pink", "Valentine"
                Return ImageCollectionManager.ImageIconsOtherFucsia.Images(8)
                'Lila
            Case "The Bezier", "High Contrast", "Seven", "Blue", "Devexpress Style", "Foggy", "Lilian", "Liquid Sky", "London Liquid Sky", "Money Twins", "Office 2007 Blue", "Office 2007 Silver", "Office 2010 Blue", "Office 2013", "Office 2013 Dark Gray", "Office 2013 Light Gray", "Seven Classic", "Stardust", "The Asphalt World", "VS2010", "Whiteprint", "Xmas (Blue)", "Visual Studio 2013 Light"
                Return ImageCollectionManager.ImageIconsOtherLila.Images(8)
                'Verde
            Case "Springtime", "Glass Oceans", "iMaginary", "Metropolis", "Office 2007 Green", "Office 2010 Silver", "Summer"
                Return ImageCollectionManager.ImageIconsOtherGreen.Images(8)
            Case Else 'Azul
                If (Not theme.Equals("Office 2013 Dark Gray")) AndAlso (theme.Contains("Black") OrElse theme.Contains("Dark") OrElse theme.Contains("Oscuro") OrElse theme.Contains("Negro") OrElse theme.Contains("Sharp") OrElse theme.Contains("Pumpkin")) Then
                    Return ImageCollectionManager.ImageIconsOtherWhite.Images(8)
                Else
                    Return ImageCollectionManager.ImageIconsOtherLila.Images(8)
                End If
        End Select
    End Function

    ''' <summary>
    ''' Obtiene la imagen del boton de cambiar contraseña
    ''' </summary>
    ''' <param name="theme">Nombre del tema a usar</param>
    ''' <returns>Imagen a usar</returns>
    Public Shared Function GetChangePasswdButtonImageByTheme(ByVal theme As String) As Image
        Select Case theme
            'Blancos
            Case "Sharp Plus", "Office 2007 Black", "Black", "Blueprint", "Dakroom", "Dark Side", "Dark Style", "Metropolis Dark", "Office 2010 Black", "Pumpkim", "Visual Studio 2013 Dark"
                Return ImageCollectionManager.ImageIconsOtherWhite.Images(2)
                'Cafe
            Case "Caramel", "Coffee"
                Return ImageCollectionManager.ImageIconsOtherBrown.Images(2)
                'Fucsia
            Case "Office 2007 Pink", "Valentine"
                Return ImageCollectionManager.ImageIconsOtherFucsia.Images(2)
                'Lila
            Case "The Bezier", "High Contrast", "Seven", "Blue", "Devexpress Style", "Foggy", "Lilian", "Liquid Sky", "London Liquid Sky", "Money Twins", "Office 2007 Blue", "Office 2007 Silver", "Office 2010 Blue", "Office 2013", "Office 2013 Dark Gray", "Office 2013 Light Gray", "Seven Classic", "Stardust", "The Asphalt World", "VS2010", "Whiteprint", "Xmas (Blue)", "Visual Studio 2013 Light"
                Return ImageCollectionManager.ImageIconsOtherLila.Images(2)
                'Verde
            Case "Springtime", "Glass Oceans", "iMaginary", "Metropolis", "Office 2007 Green", "Office 2010 Silver", "Summer"
                Return ImageCollectionManager.ImageIconsOtherGreen.Images(2)
            Case Else 'Azul
                If (Not theme.Equals("Office 2013 Dark Gray")) AndAlso (theme.Contains("Black") OrElse theme.Contains("Dark") OrElse theme.Contains("Oscuro") OrElse theme.Contains("Negro") OrElse theme.Contains("Sharp") OrElse theme.Contains("Pumpkin")) Then
                    Return ImageCollectionManager.ImageIconsOtherWhite.Images(2)
                Else
                    Return ImageCollectionManager.ImageIconsOtherLila.Images(2)
                End If
        End Select
    End Function

    ''' <summary>
    ''' Obtiene la imagen del boton
    ''' </summary>
    ''' <param name="theme">Nombre del tema a usar</param>
    ''' <param name="_index">Indice de la imagen</param>
    ''' <returns>Imagen a usar</returns>
    Public Shared Function GetImageByTheme(ByVal theme As String, ByVal _index As Byte) As Image
        Select Case theme
            'Blancos
            Case "Sharp Plus", "Office 2007 Black", "Black", "Blueprint", "Dakroom", "Dark Side", "Dark Style", "Metropolis Dark", "Office 2010 Black", "Pumpkim", "Visual Studio 2013 Dark"
                Return ImageCollectionManager.ImageIconsOtherWhite.Images(_index)
                'Cafe
            Case "Caramel", "Coffee"
                Return ImageCollectionManager.ImageIconsOtherBrown.Images(_index)
                'Fucsia
            Case "Office 2007 Pink", "Valentine"
                Return ImageCollectionManager.ImageIconsOtherFucsia.Images(_index)
                'Lila
            Case "The Bezier", "High Contrast", "Seven", "Blue", "Devexpress Style", "Foggy", "Lilian", "Liquid Sky", "London Liquid Sky", "Money Twins", "Office 2007 Blue", "Office 2007 Silver", "Office 2010 Blue", "Office 2013", "Office 2013 Dark Gray", "Office 2013 Light Gray", "Seven Classic", "Stardust", "The Asphalt World", "VS2010", "Whiteprint", "Xmas (Blue)", "Visual Studio 2013 Light"
                Return ImageCollectionManager.ImageIconsOtherLila.Images(_index)
                'Verde
            Case "Springtime", "Glass Oceans", "iMaginary", "Metropolis", "Office 2007 Green", "Office 2010 Silver", "Summer"
                Return ImageCollectionManager.ImageIconsOtherGreen.Images(_index)
            Case Else 'Azul
                If (Not theme.Equals("Office 2013 Dark Gray")) AndAlso (theme.Contains("Black") OrElse theme.Contains("Dark") OrElse theme.Contains("Oscuro") OrElse theme.Contains("Negro") OrElse theme.Contains("Sharp") OrElse theme.Contains("Pumpkin")) Then
                    Return ImageCollectionManager.ImageIconsOtherWhite.Images(_index)
                Else
                    Return ImageCollectionManager.ImageIconsOtherLila.Images(_index)
                End If

        End Select
    End Function

    ''' <summary>
    ''' Obtiene la imagen del boton
    ''' </summary>
    ''' <param name="theme">Nombre del tema a usar</param>
    ''' <returns>Imagen a usar</returns>
    Public Shared Function GetImageByThemeChangePerfil(ByVal theme As String) As Image
        Select Case theme
            'Blancos
            Case "Sharp Plus", "Office 2007 Black", "Black", "Blueprint", "Dakroom", "Dark Side", "Dark Style", "Metropolis Dark", "Office 2010 Black", "Pumpkim", "Visual Studio 2013 Dark", "Office 2019 Colorful", "The Bezier", "High Contrast"
                Return ImageCollectionManager.ImageIconsOtherWhite.Images(5)
                'Cafe
            Case "Caramel", "Coffee"
                Return ImageCollectionManager.ImageIconsOtherBrown.Images(5)
                'Fucsia
            Case "Office 2007 Pink", "Valentine"
                Return ImageCollectionManager.ImageIconsOtherFucsia.Images(5)
                'Lila
            Case "Seven", "Blue", "Devexpress Style", "Foggy", "Lilian", "Liquid Sky", "London Liquid Sky", "Money Twins", "Office 2007 Blue", "Office 2007 Silver", "Office 2010 Blue", "Office 2013", "Office 2013 Dark Gray", "Office 2013 Light Gray", "Seven Classic", "Stardust", "The Asphalt World", "VS2010", "Whiteprint", "Xmas (Blue)", "Visual Studio 2013 Light"
                Return ImageCollectionManager.ImageIconsOtherLila.Images(5)
                'Verde
            Case "Springtime", "Glass Oceans", "iMaginary", "Metropolis", "Office 2007 Green", "Office 2010 Silver", "Summer"
                Return ImageCollectionManager.ImageIconsOtherGreen.Images(5)
            Case Else 'Azul
                If (Not theme.Equals("Office 2013 Dark Gray")) AndAlso (theme.Contains("Black") OrElse theme.Contains("Dark") OrElse theme.Contains("Oscuro") OrElse theme.Contains("Negro") OrElse theme.Contains("Sharp") OrElse theme.Contains("Pumpkin")) Then
                    Return ImageCollectionManager.ImageIconsOtherWhite.Images(5)
                Else
                    Return ImageCollectionManager.ImageIconsOtherLila.Images(5)
                End If

        End Select
    End Function

    ''' <summary>
    ''' Obtiene la imagen del boton de adjuntar archivo
    ''' </summary>
    ''' <param name="theme">Nombre del tema a usar</param>
    ''' <returns>Imagen a usar</returns>
    Public Shared Function GetAttachImageButtonImageByTheme(ByVal theme As String) As Image
        Select Case theme
            'Blancos
            Case "Sharp Plus", "Office 2007 Black", "Black", "Blueprint", "Dakroom", "Dark Side", "Dark Style", "Metropolis Dark", "Office 2010 Black", "Pumpkim", "Visual Studio 2013 Dark"
                Return ImageCollectionManager.ToolBarIconsWhiteLarge.Images(29)
                'Cafe
            Case "Caramel", "Coffee"
                Return ImageCollectionManager.ToolBarIconsBrownLarge.Images(29)
                'Fucsia
            Case "Office 2007 Pink", "Valentine"
                Return ImageCollectionManager.ToolBarIconsFucsiaLarge.Images(29)
                'Lila
            Case "The Bezier", "High Contrast", "Seven", "Blue", "Devexpress Style", "Foggy", "Lilian", "Liquid Sky", "London Liquid Sky", "Money Twins", "Office 2007 Blue", "Office 2007 Silver", "Office 2010 Blue", "Office 2013", "Office 2013 Dark Gray", "Office 2013 Light Gray", "Seven Classic", "Stardust", "The Asphalt World", "VS2010", "Whiteprint", "Xmas (Blue)", "Visual Studio 2013 Light"
                Return ImageCollectionManager.ToolBarIconsLilaLarge.Images(29)
                'Verde
            Case "Springtime", "Glass Oceans", "iMaginary", "Metropolis", "Office 2007 Green", "Office 2010 Silver", "Summer"
                Return ImageCollectionManager.ToolBarIconsGreenLarge.Images(29)
            Case Else 'Azul
                If (Not theme.Equals("Office 2013 Dark Gray")) AndAlso (theme.Contains("Black") OrElse theme.Contains("Dark") OrElse theme.Contains("Oscuro") OrElse theme.Contains("Negro") OrElse theme.Contains("Sharp") OrElse theme.Contains("Pumpkin")) Then
                    Return ImageCollectionManager.ToolBarIconsWhiteLarge.Images(29)
                Else
                    Return ImageCollectionManager.ToolBarIconsLilaLarge.Images(29)
                End If
        End Select
    End Function

    ''' <summary>
    ''' Obtiene la imagen del boton de escanear
    ''' </summary>
    ''' <param name="theme">Nombre del tema a usar</param>
    ''' <returns>Imagen a usar</returns>
    Public Shared Function GetScanImageButtonImageByTheme(ByVal theme As String) As Image
        Select Case theme
            'Blancos
            Case "Sharp Plus", "Office 2007 Black", "Black", "Blueprint", "Dakroom", "Dark Side", "Dark Style", "Metropolis Dark", "Office 2010 Black", "Pumpkim", "Visual Studio 2013 Dark"
                Return ImageCollectionManager.ToolBarIconsWhiteLarge.Images(28)
                'Cafe
            Case "Caramel", "Coffee"
                Return ImageCollectionManager.ToolBarIconsBrownLarge.Images(28)
                'Fucsia
            Case "Office 2007 Pink", "Valentine"
                Return ImageCollectionManager.ToolBarIconsFucsiaLarge.Images(28)
                'Lila
            Case "The Bezier", "High Contrast", "Seven", "Blue", "Devexpress Style", "Foggy", "Lilian", "Liquid Sky", "London Liquid Sky", "Money Twins", "Office 2007 Blue", "Office 2007 Silver", "Office 2010 Blue", "Office 2013", "Office 2013 Dark Gray", "Office 2013 Light Gray", "Seven Classic", "Stardust", "The Asphalt World", "VS2010", "Whiteprint", "Xmas (Blue)", "Visual Studio 2013 Light"
                Return ImageCollectionManager.ToolBarIconsLilaLarge.Images(28)
                'Verde
            Case "Springtime", "Glass Oceans", "iMaginary", "Metropolis", "Office 2007 Green", "Office 2010 Silver", "Summer"
                Return ImageCollectionManager.ToolBarIconsGreenLarge.Images(28)
            Case Else 'Azul
                If (Not theme.Equals("Office 2013 Dark Gray")) AndAlso (theme.Contains("Black") OrElse theme.Contains("Dark") OrElse theme.Contains("Oscuro") OrElse theme.Contains("Negro") OrElse theme.Contains("Sharp") OrElse theme.Contains("Pumpkin")) Then
                    Return ImageCollectionManager.ToolBarIconsWhiteLarge.Images(28)
                Else
                    Return ImageCollectionManager.ToolBarIconsLilaLarge.Images(28)
                End If
        End Select
    End Function

    ''' <summary>
    ''' Obtiene la imagen del boton de bloqueo de sesión
    ''' </summary>
    ''' <param name="theme">Nombre del tema a usar</param>
    ''' <returns>Imagen a usar</returns>
    Public Shared Function GetBlockSessionButtonImageByTheme(ByVal theme As String) As Image
        Select Case theme
            'Blancos
            Case "Sharp Plus", "Office 2007 Black", "Black", "Blueprint", "Dakroom", "Dark Side", "Dark Style", "Metropolis Dark", "Office 2010 Black", "Pumpkim", "Visual Studio 2013 Dark"
                Return ImageCollectionManager.ImageIconsOtherWhite.Images(4)
                'Cafe
            Case "Caramel", "Coffee"
                Return ImageCollectionManager.ImageIconsOtherBrown.Images(4)
                'Fucsia
            Case "Office 2007 Pink", "Valentine"
                Return ImageCollectionManager.ImageIconsOtherFucsia.Images(4)
                'Lila
            Case "The Bezier", "High Contrast", "Seven", "Blue", "Devexpress Style", "Foggy", "Lilian", "Liquid Sky", "London Liquid Sky", "Money Twins", "Office 2007 Blue", "Office 2007 Silver", "Office 2010 Blue", "Office 2013", "Office 2013 Dark Gray", "Office 2013 Light Gray", "Seven Classic", "Stardust", "The Asphalt World", "VS2010", "Whiteprint", "Xmas (Blue)", "Visual Studio 2013 Light"
                Return ImageCollectionManager.ImageIconsOtherLila.Images(4)
                'Verde
            Case "Springtime", "Glass Oceans", "iMaginary", "Metropolis", "Office 2007 Green", "Office 2010 Silver", "Summer"
                Return ImageCollectionManager.ImageIconsOtherGreen.Images(4)
            Case Else 'Azul
                If (Not theme.Equals("Office 2013 Dark Gray")) AndAlso (theme.Contains("Black") OrElse theme.Contains("Dark") OrElse theme.Contains("Oscuro") OrElse theme.Contains("Negro") OrElse theme.Contains("Sharp") OrElse theme.Contains("Pumpkin")) Then
                    Return ImageCollectionManager.ImageIconsOtherWhite.Images(4)
                Else
                    Return ImageCollectionManager.ImageIconsOtherLila.Images(4)
                End If
        End Select
    End Function

    ''' <summary>
    ''' Obtiene la imagen del boton de notificaciones
    ''' </summary>
    ''' <param name="theme">Nombre del tema a usar</param>
    ''' <returns>Imagen a usar</returns>
    Public Shared Function GetNotificationButtonImageByTheme(ByVal theme As String) As Image
        Select Case theme
            'Blancos
            Case "Sharp Plus", "Office 2007 Black", "Black", "Blueprint", "Dakroom", "Dark Side", "Dark Style", "Metropolis Dark", "Office 2010 Black", "Pumpkim", "Visual Studio 2013 Dark", "Office 2019 Colorful", "The Bezier", "High Contrast"
                Return ImageCollectionManager.ImageIconsOtherWhite.Images(10)
                'Cafe
            Case "Caramel", "Coffee"
                Return ImageCollectionManager.ImageIconsOtherBrown.Images(10)
                'Fucsia
            Case "Office 2007 Pink", "Valentine"
                Return ImageCollectionManager.ImageIconsOtherFucsia.Images(10)
                'Lila
            Case "Seven", "Blue", "Devexpress Style", "Foggy", "Lilian", "Liquid Sky", "London Liquid Sky", "Money Twins", "Office 2007 Blue", "Office 2007 Silver", "Office 2010 Blue", "Office 2013", "Office 2013 Dark Gray", "Office 2013 Light Gray", "Seven Classic", "Stardust", "The Asphalt World", "VS2010", "Whiteprint", "Xmas (Blue)", "Visual Studio 2013 Light"
                Return ImageCollectionManager.ImageIconsOtherLila.Images(10)
                'Verde
            Case "Springtime", "Glass Oceans", "iMaginary", "Metropolis", "Office 2007 Green", "Office 2010 Silver", "Summer"
                Return ImageCollectionManager.ImageIconsOtherGreen.Images(10)
            Case Else 'Azul
                'Return ImageCollectionManager.ImageIconsOtherBlue.Images(1)
                If (Not theme.Equals("Office 2013 Dark Gray")) AndAlso (theme.Contains("Black") OrElse theme.Contains("Dark") OrElse theme.Contains("Oscuro") OrElse theme.Contains("Negro") OrElse theme.Contains("Sharp") OrElse theme.Contains("Pumpkin")) Then
                    Return ImageCollectionManager.ImageIconsOtherWhite.Images(10)
                Else
                    Return ImageCollectionManager.ImageIconsOtherLila.Images(10)
                End If
        End Select
    End Function

    ''' <summary>
    ''' Obtiene la imagen del boton de configuración
    ''' </summary>
    ''' <param name="theme">Nombre del tema a usar</param>
    ''' <returns>Imagen a usar</returns>
    Public Shared Function GetConfigButtonImageByTheme(ByVal theme As String) As Image
        Select Case theme
            'Blancos
            Case "Sharp Plus", "Office 2007 Black", "Black", "Blueprint", "Dakroom", "Dark Side", "Dark Style", "Metropolis Dark", "Office 2010 Black", "Pumpkim", "Visual Studio 2013 Dark", "Office 2019 Colorful", "The Bezier", "High Contrast"
                Return ImageCollectionManager.ImageIconsOtherWhite.Images(9)
                'Camel
            Case "Caramel", "Coffee"
                Return ImageCollectionManager.ImageIconsOtherBrown.Images(9)
                'Fucsia
            Case "Office 2007 Pink", "Valentine"
                Return ImageCollectionManager.ImageIconsOtherFucsia.Images(9)
                'Lila
            Case "Seven", "Blue", "Devexpress Style", "Foggy", "Lilian", "Liquid Sky", "London Liquid Sky", "Money Twins", "Office 2007 Blue", "Office 2007 Silver", "Office 2010 Blue", "Office 2013", "Office 2013 Dark Gray", "Office 2013 Light Gray", "Seven Classic", "Stardust", "The Asphalt World", "VS2010", "Whiteprint", "Xmas (Blue)", "Visual Studio 2013 Light"
                Return ImageCollectionManager.ImageIconsOtherLila.Images(9)
                'Verde
            Case "Springtime", "Glass Oceans", "iMaginary", "Metropolis", "Office 2007 Green", "Office 2010 Silver", "Summer"
                Return ImageCollectionManager.ImageIconsOtherGreen.Images(9)
            Case Else 'Azul
                'Return ImageCollectionManager.ImageIconsOtherBlue.Images(0)
                If (Not theme.Equals("Office 2013 Dark Gray")) AndAlso (theme.Contains("Black") OrElse theme.Contains("Dark") OrElse theme.Contains("Oscuro") OrElse theme.Contains("Negro") OrElse theme.Contains("Sharp") OrElse theme.Contains("Pumpkin")) Then
                    Return ImageCollectionManager.ImageIconsOtherWhite.Images(9)
                Else
                    Return ImageCollectionManager.ImageIconsOtherLila.Images(9)
                End If
        End Select
    End Function

    ''' <summary>
    ''' Obtiene la imagen del logo de vituel
    ''' </summary>
    ''' <param name="theme">Nombre del tema a usar</param>
    ''' <returns>Imagen a usar</returns>
    Public Shared Function GetVituelImageByTheme(ByVal theme As String) As Image
        Select Case theme
            'Blancos
            Case "Sharp Plus", "Office 2007 Black", "Black", "Blueprint", "Dakroom", "Dark Side", "Dark Style", "Metropolis Dark", "Office 2010 Black", "Pumpkim", "Visual Studio 2013 Dark", "Office 2019 Colorful", "The Bezier", "High Contrast"
                Return ImageCollectionManager.ImageIconsOtherWhite.Images(13)
                'cafe
            Case "Caramel", "Coffee"
                Return ImageCollectionManager.ImageIconsOtherBrown.Images(13)
                'Fucsia
            Case "Office 2007 Pink", "Valentine"
                Return ImageCollectionManager.ImageIconsOtherFucsia.Images(13)
                'Lila
            Case "Seven", "Blue", "Devexpress Style", "Foggy", "Lilian", "Liquid Sky", "London Liquid Sky", "Money Twins", "Office 2007 Blue", "Office 2007 Silver", "Office 2010 Blue", "Office 2013", "Office 2013 Dark Gray", "Office 2013 Light Gray", "Seven Classic", "Stardust", "The Asphalt World", "VS2010", "Whiteprint", "Xmas (Blue)", "Visual Studio 2013 Light"
                Return ImageCollectionManager.ImageIconsOtherLila.Images(13)
                'Verde
            Case "Springtime", "Glass Oceans", "iMaginary", "Metropolis", "Office 2007 Green", "Office 2010 Silver", "Summer"
                Return ImageCollectionManager.ImageIconsOtherGreen.Images(13)
            Case Else 'Azul
                'Return ImageCollectionManager.ImageIconsOtherBlue.Images(2)
                If (Not theme.Equals("Office 2013 Dark Gray")) AndAlso (theme.Contains("Black") OrElse theme.Contains("Dark") OrElse theme.Contains("Oscuro") OrElse theme.Contains("Negro") OrElse theme.Contains("Sharp") OrElse theme.Contains("Pumpkin")) Then
                    Return ImageCollectionManager.ImageIconsOtherWhite.Images(13)
                Else
                    Return ImageCollectionManager.ImageIconsOtherLila.Images(13)
                End If
        End Select
    End Function

    ''' <summary>
    ''' Obtiene la imagen del logo en el boton inicio de la aplicación
    ''' </summary>
    ''' <param name="theme">Nombre del tema a usar</param>
    ''' <returns>Imagen a usar</returns>
    Public Shared Function GetLogoImageByTheme(ByVal theme As String) As SvgImage
        If (Not theme.Equals("Office 2013 Dark Gray")) AndAlso (theme.Contains("Black") OrElse theme.Contains("Dark") OrElse theme.Contains("Oscuro") OrElse theme.Contains("Negro") OrElse theme.Contains("Sharp") OrElse theme.Contains("Pumpkin") OrElse
            theme.Contains("High Contrast")) Then
            Return My.Resources.formapplicationbutton11
        Else
            Return My.Resources.formapplicationbutton2

        End If
    End Function

    ''' <summary>
    ''' Obtiene la imagen del logo en el boton inicio de la aplicación
    ''' </summary>
    ''' <param name="theme">Nombre del tema a usar</param>
    ''' <returns>Imagen a usar</returns>
    Public Shared Function GetLogoImageByThemePng(ByVal theme As String) As Image
        If (Not theme.Equals("Office 2013 Dark Gray")) AndAlso (theme.Contains("Black") OrElse theme.Contains("Dark") OrElse theme.Contains("Oscuro") OrElse theme.Contains("Negro") OrElse theme.Contains("Sharp") OrElse theme.Contains("Pumpkin") OrElse
            theme.Contains("High Contrast") OrElse theme.Contains("Caramel") OrElse theme.Contains("Coffee")) Then
            Return My.Resources.formapplicationbutton
        Else
            Return My.Resources.formapplicationbutton1
        End If
    End Function

#End Region

End Class