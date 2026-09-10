'***********************************************************************'
' Assembly         : Presentation.Controls                              '
' Author           : Jorge Leonardo Vernaza                             '
' Created          : 01-10-2012                                         '
'                                                                       '
' Last Modified By :                                                    '
' Last Modified On :                                                    '
' Description      :                                                    '
'                                                                       '
' Copyright        : (c) . All rights reserved.                         '
'***********************************************************************'
Imports System.Collections.Generic

Public Class Metadata
    Public Class Metadata
        Public Property result_type() As String
            Get
                Return m_result_type
            End Get
            Set(value As String)
                m_result_type = Value
            End Set
        End Property
        Private m_result_type As String
        Public Property iso_language_code() As String
            Get
                Return m_iso_language_code
            End Get
            Set(value As String)
                m_iso_language_code = Value
            End Set
        End Property
        Private m_iso_language_code As String
    End Class

    Public Class Description
        Public Property urls() As List(Of Object)
            Get
                Return m_urls
            End Get
            Set(value As List(Of Object))
                m_urls = Value
            End Set
        End Property
        Private m_urls As List(Of Object)
    End Class

    Public Class Url2
        Public Property url() As String
            Get
                Return m_url
            End Get
            Set(value As String)
                m_url = Value
            End Set
        End Property
        Private m_url As String
        Public Property expanded_url() As String
            Get
                Return m_expanded_url
            End Get
            Set(value As String)
                m_expanded_url = Value
            End Set
        End Property
        Private m_expanded_url As String
        Public Property display_url() As String
            Get
                Return m_display_url
            End Get
            Set(value As String)
                m_display_url = Value
            End Set
        End Property
        Private m_display_url As String
        Public Property indices() As List(Of Integer)
            Get
                Return m_indices
            End Get
            Set(value As List(Of Integer))
                m_indices = Value
            End Set
        End Property
        Private m_indices As List(Of Integer)
    End Class

    Public Class Url
        Public Property urls() As List(Of Url2)
            Get
                Return m_urls
            End Get
            Set(value As List(Of Url2))
                m_urls = Value
            End Set
        End Property
        Private m_urls As List(Of Url2)
    End Class

    Public Class Entities
        Public Property description() As Description
            Get
                Return m_description
            End Get
            Set(value As Description)
                m_description = Value
            End Set
        End Property
        Private m_description As Description
        Public Property url() As Url
            Get
                Return m_url
            End Get
            Set(value As Url)
                m_url = Value
            End Set
        End Property
        Private m_url As Url
    End Class

    Public Class User
        Public Property id() As Integer
            Get
                Return m_id
            End Get
            Set(value As Integer)
                m_id = Value
            End Set
        End Property
        Private m_id As Integer
        Public Property id_str() As String
            Get
                Return m_id_str
            End Get
            Set(value As String)
                m_id_str = Value
            End Set
        End Property
        Private m_id_str As String
        Public Property name() As String
            Get
                Return m_name
            End Get
            Set(value As String)
                m_name = Value
            End Set
        End Property
        Private m_name As String
        Public Property screen_name() As String
            Get
                Return m_screen_name
            End Get
            Set(value As String)
                m_screen_name = Value
            End Set
        End Property
        Private m_screen_name As String
        Public Property location() As String
            Get
                Return m_location
            End Get
            Set(value As String)
                m_location = Value
            End Set
        End Property
        Private m_location As String
        Public Property description() As String
            Get
                Return m_description
            End Get
            Set(value As String)
                m_description = Value
            End Set
        End Property
        Private m_description As String
        Public Property url() As String
            Get
                Return m_url
            End Get
            Set(value As String)
                m_url = Value
            End Set
        End Property
        Private m_url As String
        Public Property entities() As Entities
            Get
                Return m_entities
            End Get
            Set(value As Entities)
                m_entities = Value
            End Set
        End Property
        Private m_entities As Entities
        Public Property [protected]() As Boolean
            Get
                Return m_protected
            End Get
            Set(value As Boolean)
                m_protected = Value
            End Set
        End Property
        Private m_protected As Boolean
        Public Property followers_count() As Integer
            Get
                Return m_followers_count
            End Get
            Set(value As Integer)
                m_followers_count = Value
            End Set
        End Property
        Private m_followers_count As Integer
        Public Property friends_count() As Integer
            Get
                Return m_friends_count
            End Get
            Set(value As Integer)
                m_friends_count = Value
            End Set
        End Property
        Private m_friends_count As Integer
        Public Property listed_count() As Integer
            Get
                Return m_listed_count
            End Get
            Set(value As Integer)
                m_listed_count = Value
            End Set
        End Property
        Private m_listed_count As Integer
        Public Property created_at() As String
            Get
                Return m_created_at
            End Get
            Set(value As String)
                m_created_at = Value
            End Set
        End Property
        Private m_created_at As String
        Public Property favourites_count() As Integer
            Get
                Return m_favourites_count
            End Get
            Set(value As Integer)
                m_favourites_count = Value
            End Set
        End Property
        Private m_favourites_count As Integer
        Public Property utc_offset() As System.Nullable(Of Integer)
            Get
                Return m_utc_offset
            End Get
            Set(value As System.Nullable(Of Integer))
                m_utc_offset = Value
            End Set
        End Property
        Private m_utc_offset As System.Nullable(Of Integer)
        Public Property time_zone() As String
            Get
                Return m_time_zone
            End Get
            Set(value As String)
                m_time_zone = Value
            End Set
        End Property
        Private m_time_zone As String
        Public Property geo_enabled() As Boolean
            Get
                Return m_geo_enabled
            End Get
            Set(value As Boolean)
                m_geo_enabled = Value
            End Set
        End Property
        Private m_geo_enabled As Boolean
        Public Property verified() As Boolean
            Get
                Return m_verified
            End Get
            Set(value As Boolean)
                m_verified = Value
            End Set
        End Property
        Private m_verified As Boolean
        Public Property statuses_count() As Integer
            Get
                Return m_statuses_count
            End Get
            Set(value As Integer)
                m_statuses_count = Value
            End Set
        End Property
        Private m_statuses_count As Integer
        Public Property lang() As String
            Get
                Return m_lang
            End Get
            Set(value As String)
                m_lang = Value
            End Set
        End Property
        Private m_lang As String
        Public Property contributors_enabled() As Boolean
            Get
                Return m_contributors_enabled
            End Get
            Set(value As Boolean)
                m_contributors_enabled = Value
            End Set
        End Property
        Private m_contributors_enabled As Boolean
        Public Property is_translator() As Boolean
            Get
                Return m_is_translator
            End Get
            Set(value As Boolean)
                m_is_translator = Value
            End Set
        End Property
        Private m_is_translator As Boolean
        Public Property profile_background_color() As String
            Get
                Return m_profile_background_color
            End Get
            Set(value As String)
                m_profile_background_color = Value
            End Set
        End Property
        Private m_profile_background_color As String
        Public Property profile_background_image_url() As String
            Get
                Return m_profile_background_image_url
            End Get
            Set(value As String)
                m_profile_background_image_url = Value
            End Set
        End Property
        Private m_profile_background_image_url As String
        Public Property profile_background_image_url_https() As String
            Get
                Return m_profile_background_image_url_https
            End Get
            Set(value As String)
                m_profile_background_image_url_https = Value
            End Set
        End Property
        Private m_profile_background_image_url_https As String
        Public Property profile_background_tile() As Boolean
            Get
                Return m_profile_background_tile
            End Get
            Set(value As Boolean)
                m_profile_background_tile = Value
            End Set
        End Property
        Private m_profile_background_tile As Boolean
        Public Property profile_image_url() As String
            Get
                Return m_profile_image_url
            End Get
            Set(value As String)
                m_profile_image_url = Value
            End Set
        End Property
        Private m_profile_image_url As String
        Public Property profile_image_url_https() As String
            Get
                Return m_profile_image_url_https
            End Get
            Set(value As String)
                m_profile_image_url_https = Value
            End Set
        End Property
        Private m_profile_image_url_https As String
        Public Property profile_banner_url() As String
            Get
                Return m_profile_banner_url
            End Get
            Set(value As String)
                m_profile_banner_url = Value
            End Set
        End Property
        Private m_profile_banner_url As String
        Public Property profile_link_color() As String
            Get
                Return m_profile_link_color
            End Get
            Set(value As String)
                m_profile_link_color = Value
            End Set
        End Property
        Private m_profile_link_color As String
        Public Property profile_sidebar_border_color() As String
            Get
                Return m_profile_sidebar_border_color
            End Get
            Set(value As String)
                m_profile_sidebar_border_color = Value
            End Set
        End Property
        Private m_profile_sidebar_border_color As String
        Public Property profile_sidebar_fill_color() As String
            Get
                Return m_profile_sidebar_fill_color
            End Get
            Set(value As String)
                m_profile_sidebar_fill_color = Value
            End Set
        End Property
        Private m_profile_sidebar_fill_color As String
        Public Property profile_text_color() As String
            Get
                Return m_profile_text_color
            End Get
            Set(value As String)
                m_profile_text_color = Value
            End Set
        End Property
        Private m_profile_text_color As String
        Public Property profile_use_background_image() As Boolean
            Get
                Return m_profile_use_background_image
            End Get
            Set(value As Boolean)
                m_profile_use_background_image = Value
            End Set
        End Property
        Private m_profile_use_background_image As Boolean
        Public Property default_profile() As Boolean
            Get
                Return m_default_profile
            End Get
            Set(value As Boolean)
                m_default_profile = Value
            End Set
        End Property
        Private m_default_profile As Boolean
        Public Property default_profile_image() As Boolean
            Get
                Return m_default_profile_image
            End Get
            Set(value As Boolean)
                m_default_profile_image = Value
            End Set
        End Property
        Private m_default_profile_image As Boolean
        Public Property following() As Boolean
            Get
                Return m_following
            End Get
            Set(value As Boolean)
                m_following = Value
            End Set
        End Property
        Private m_following As Boolean
        Public Property follow_request_sent() As Boolean
            Get
                Return m_follow_request_sent
            End Get
            Set(value As Boolean)
                m_follow_request_sent = Value
            End Set
        End Property
        Private m_follow_request_sent As Boolean
        Public Property notifications() As Boolean
            Get
                Return m_notifications
            End Get
            Set(value As Boolean)
                m_notifications = Value
            End Set
        End Property
        Private m_notifications As Boolean
    End Class

    Public Class Geo
        Public Property type() As String
            Get
                Return m_type
            End Get
            Set(value As String)
                m_type = Value
            End Set
        End Property
        Private m_type As String
        Public Property coordinates() As List(Of Double)
            Get
                Return m_coordinates
            End Get
            Set(value As List(Of Double))
                m_coordinates = Value
            End Set
        End Property
        Private m_coordinates As List(Of Double)
    End Class

    Public Class Coordinates
        Public Property type() As String
            Get
                Return m_type
            End Get
            Set(value As String)
                m_type = Value
            End Set
        End Property
        Private m_type As String
        Public Property coordinates() As List(Of Double)
            Get
                Return m_coordinates
            End Get
            Set(value As List(Of Double))
                m_coordinates = Value
            End Set
        End Property
        Private m_coordinates As List(Of Double)
    End Class

    Public Class BoundingBox
        Public Property type() As String
            Get
                Return m_type
            End Get
            Set(value As String)
                m_type = Value
            End Set
        End Property
        Private m_type As String
        Public Property coordinates() As List(Of List(Of List(Of Double)))
            Get
                Return m_coordinates
            End Get
            Set(value As List(Of List(Of List(Of Double))))
                m_coordinates = Value
            End Set
        End Property
        Private m_coordinates As List(Of List(Of List(Of Double)))
    End Class

    Public Class Attributes
    End Class

    Public Class Place
        Public Property id() As String
            Get
                Return m_id
            End Get
            Set(value As String)
                m_id = Value
            End Set
        End Property
        Private m_id As String
        Public Property url() As String
            Get
                Return m_url
            End Get
            Set(value As String)
                m_url = Value
            End Set
        End Property
        Private m_url As String
        Public Property place_type() As String
            Get
                Return m_place_type
            End Get
            Set(value As String)
                m_place_type = Value
            End Set
        End Property
        Private m_place_type As String
        Public Property name() As String
            Get
                Return m_name
            End Get
            Set(value As String)
                m_name = Value
            End Set
        End Property
        Private m_name As String
        Public Property full_name() As String
            Get
                Return m_full_name
            End Get
            Set(value As String)
                m_full_name = Value
            End Set
        End Property
        Private m_full_name As String
        Public Property country_code() As String
            Get
                Return m_country_code
            End Get
            Set(value As String)
                m_country_code = Value
            End Set
        End Property
        Private m_country_code As String
        Public Property country() As String
            Get
                Return m_country
            End Get
            Set(value As String)
                m_country = Value
            End Set
        End Property
        Private m_country As String
        Public Property bounding_box() As BoundingBox
            Get
                Return m_bounding_box
            End Get
            Set(value As BoundingBox)
                m_bounding_box = Value
            End Set
        End Property
        Private m_bounding_box As BoundingBox
        Public Property attributes() As Attributes
            Get
                Return m_attributes
            End Get
            Set(value As Attributes)
                m_attributes = Value
            End Set
        End Property
        Private m_attributes As Attributes
    End Class

    Public Class Large
        Public Property w() As Integer
            Get
                Return m_w
            End Get
            Set(value As Integer)
                m_w = Value
            End Set
        End Property
        Private m_w As Integer
        Public Property h() As Integer
            Get
                Return m_h
            End Get
            Set(value As Integer)
                m_h = Value
            End Set
        End Property
        Private m_h As Integer
        Public Property resize() As String
            Get
                Return m_resize
            End Get
            Set(value As String)
                m_resize = Value
            End Set
        End Property
        Private m_resize As String
    End Class

    Public Class Medium2
        Public Property w() As Integer
            Get
                Return m_w
            End Get
            Set(value As Integer)
                m_w = Value
            End Set
        End Property
        Private m_w As Integer
        Public Property h() As Integer
            Get
                Return m_h
            End Get
            Set(value As Integer)
                m_h = Value
            End Set
        End Property
        Private m_h As Integer
        Public Property resize() As String
            Get
                Return m_resize
            End Get
            Set(value As String)
                m_resize = Value
            End Set
        End Property
        Private m_resize As String
    End Class

    Public Class Thumb
        Public Property w() As Integer
            Get
                Return m_w
            End Get
            Set(value As Integer)
                m_w = Value
            End Set
        End Property
        Private m_w As Integer
        Public Property h() As Integer
            Get
                Return m_h
            End Get
            Set(value As Integer)
                m_h = Value
            End Set
        End Property
        Private m_h As Integer
        Public Property resize() As String
            Get
                Return m_resize
            End Get
            Set(value As String)
                m_resize = Value
            End Set
        End Property
        Private m_resize As String
    End Class

    Public Class Small
        Public Property w() As Integer
            Get
                Return m_w
            End Get
            Set(value As Integer)
                m_w = Value
            End Set
        End Property
        Private m_w As Integer
        Public Property h() As Integer
            Get
                Return m_h
            End Get
            Set(value As Integer)
                m_h = Value
            End Set
        End Property
        Private m_h As Integer
        Public Property resize() As String
            Get
                Return m_resize
            End Get
            Set(value As String)
                m_resize = Value
            End Set
        End Property
        Private m_resize As String
    End Class

    Public Class Sizes
        Public Property large() As Large
            Get
                Return m_large
            End Get
            Set(value As Large)
                m_large = Value
            End Set
        End Property
        Private m_large As Large
        Public Property medium() As Medium2
            Get
                Return m_medium
            End Get
            Set(value As Medium2)
                m_medium = Value
            End Set
        End Property
        Private m_medium As Medium2
        Public Property thumb() As Thumb
            Get
                Return m_thumb
            End Get
            Set(value As Thumb)
                m_thumb = Value
            End Set
        End Property
        Private m_thumb As Thumb
        Public Property small() As Small
            Get
                Return m_small
            End Get
            Set(value As Small)
                m_small = Value
            End Set
        End Property
        Private m_small As Small
    End Class

    Public Class Medium
        Public Property id() As Long
            Get
                Return m_id
            End Get
            Set(value As Long)
                m_id = Value
            End Set
        End Property
        Private m_id As Long
        Public Property id_str() As String
            Get
                Return m_id_str
            End Get
            Set(value As String)
                m_id_str = Value
            End Set
        End Property
        Private m_id_str As String
        Public Property indices() As List(Of Integer)
            Get
                Return m_indices
            End Get
            Set(value As List(Of Integer))
                m_indices = Value
            End Set
        End Property
        Private m_indices As List(Of Integer)
        Public Property media_url() As String
            Get
                Return m_media_url
            End Get
            Set(value As String)
                m_media_url = Value
            End Set
        End Property
        Private m_media_url As String
        Public Property media_url_https() As String
            Get
                Return m_media_url_https
            End Get
            Set(value As String)
                m_media_url_https = Value
            End Set
        End Property
        Private m_media_url_https As String
        Public Property url() As String
            Get
                Return m_url
            End Get
            Set(value As String)
                m_url = Value
            End Set
        End Property
        Private m_url As String
        Public Property display_url() As String
            Get
                Return m_display_url
            End Get
            Set(value As String)
                m_display_url = Value
            End Set
        End Property
        Private m_display_url As String
        Public Property expanded_url() As String
            Get
                Return m_expanded_url
            End Get
            Set(value As String)
                m_expanded_url = Value
            End Set
        End Property
        Private m_expanded_url As String
        Public Property type() As String
            Get
                Return m_type
            End Get
            Set(value As String)
                m_type = Value
            End Set
        End Property
        Private m_type As String
        Public Property sizes() As Sizes
            Get
                Return m_sizes
            End Get
            Set(value As Sizes)
                m_sizes = Value
            End Set
        End Property
        Private m_sizes As Sizes
    End Class

    Public Class Entities2
        Public Property hashtags() As List(Of Object)
            Get
                Return m_hashtags
            End Get
            Set(value As List(Of Object))
                m_hashtags = Value
            End Set
        End Property
        Private m_hashtags As List(Of Object)
        Public Property symbols() As List(Of Object)
            Get
                Return m_symbols
            End Get
            Set(value As List(Of Object))
                m_symbols = Value
            End Set
        End Property
        Private m_symbols As List(Of Object)
        Public Property urls() As List(Of Object)
            Get
                Return m_urls
            End Get
            Set(value As List(Of Object))
                m_urls = Value
            End Set
        End Property
        Private m_urls As List(Of Object)
        Public Property user_mentions() As List(Of Object)
            Get
                Return m_user_mentions
            End Get
            Set(value As List(Of Object))
                m_user_mentions = Value
            End Set
        End Property
        Private m_user_mentions As List(Of Object)
        Public Property media() As List(Of Medium)
            Get
                Return m_media
            End Get
            Set(value As List(Of Medium))
                m_media = Value
            End Set
        End Property
        Private m_media As List(Of Medium)
    End Class

    Public Class Metadata2
        Public Property result_type() As String
            Get
                Return m_result_type
            End Get
            Set(value As String)
                m_result_type = Value
            End Set
        End Property
        Private m_result_type As String
        Public Property iso_language_code() As String
            Get
                Return m_iso_language_code
            End Get
            Set(value As String)
                m_iso_language_code = Value
            End Set
        End Property
        Private m_iso_language_code As String
    End Class

    Public Class Url4
        Public Property url() As String
            Get
                Return m_url
            End Get
            Set(value As String)
                m_url = Value
            End Set
        End Property
        Private m_url As String
        Public Property expanded_url() As String
            Get
                Return m_expanded_url
            End Get
            Set(value As String)
                m_expanded_url = Value
            End Set
        End Property
        Private m_expanded_url As String
        Public Property display_url() As String
            Get
                Return m_display_url
            End Get
            Set(value As String)
                m_display_url = Value
            End Set
        End Property
        Private m_display_url As String
        Public Property indices() As List(Of Integer)
            Get
                Return m_indices
            End Get
            Set(value As List(Of Integer))
                m_indices = Value
            End Set
        End Property
        Private m_indices As List(Of Integer)
    End Class

    Public Class Url3
        Public Property urls() As List(Of Url4)
            Get
                Return m_urls
            End Get
            Set(value As List(Of Url4))
                m_urls = Value
            End Set
        End Property
        Private m_urls As List(Of Url4)
    End Class

    Public Class Description2
        Public Property urls() As List(Of Object)
            Get
                Return m_urls
            End Get
            Set(value As List(Of Object))
                m_urls = Value
            End Set
        End Property
        Private m_urls As List(Of Object)
    End Class

    Public Class Entities3
        Public Property url() As Url3
            Get
                Return m_url
            End Get
            Set(value As Url3)
                m_url = Value
            End Set
        End Property
        Private m_url As Url3
        Public Property description() As Description2
            Get
                Return m_description
            End Get
            Set(value As Description2)
                m_description = Value
            End Set
        End Property
        Private m_description As Description2
    End Class

    Public Class User2
        Public Property id() As Integer
            Get
                Return m_id
            End Get
            Set(value As Integer)
                m_id = Value
            End Set
        End Property
        Private m_id As Integer
        Public Property id_str() As String
            Get
                Return m_id_str
            End Get
            Set(value As String)
                m_id_str = Value
            End Set
        End Property
        Private m_id_str As String
        Public Property name() As String
            Get
                Return m_name
            End Get
            Set(value As String)
                m_name = Value
            End Set
        End Property
        Private m_name As String
        Public Property screen_name() As String
            Get
                Return m_screen_name
            End Get
            Set(value As String)
                m_screen_name = Value
            End Set
        End Property
        Private m_screen_name As String
        Public Property location() As String
            Get
                Return m_location
            End Get
            Set(value As String)
                m_location = Value
            End Set
        End Property
        Private m_location As String
        Public Property description() As String
            Get
                Return m_description
            End Get
            Set(value As String)
                m_description = Value
            End Set
        End Property
        Private m_description As String
        Public Property url() As String
            Get
                Return m_url
            End Get
            Set(value As String)
                m_url = Value
            End Set
        End Property
        Private m_url As String
        Public Property entities() As Entities3
            Get
                Return m_entities
            End Get
            Set(value As Entities3)
                m_entities = Value
            End Set
        End Property
        Private m_entities As Entities3
        Public Property [protected]() As Boolean
            Get
                Return m_protected
            End Get
            Set(value As Boolean)
                m_protected = Value
            End Set
        End Property
        Private m_protected As Boolean
        Public Property followers_count() As Integer
            Get
                Return m_followers_count
            End Get
            Set(value As Integer)
                m_followers_count = Value
            End Set
        End Property
        Private m_followers_count As Integer
        Public Property friends_count() As Integer
            Get
                Return m_friends_count
            End Get
            Set(value As Integer)
                m_friends_count = Value
            End Set
        End Property
        Private m_friends_count As Integer
        Public Property listed_count() As Integer
            Get
                Return m_listed_count
            End Get
            Set(value As Integer)
                m_listed_count = Value
            End Set
        End Property
        Private m_listed_count As Integer
        Public Property created_at() As String
            Get
                Return m_created_at
            End Get
            Set(value As String)
                m_created_at = Value
            End Set
        End Property
        Private m_created_at As String
        Public Property favourites_count() As Integer
            Get
                Return m_favourites_count
            End Get
            Set(value As Integer)
                m_favourites_count = Value
            End Set
        End Property
        Private m_favourites_count As Integer
        Public Property utc_offset() As System.Nullable(Of Integer)
            Get
                Return m_utc_offset
            End Get
            Set(value As System.Nullable(Of Integer))
                m_utc_offset = Value
            End Set
        End Property
        Private m_utc_offset As System.Nullable(Of Integer)
        Public Property time_zone() As String
            Get
                Return m_time_zone
            End Get
            Set(value As String)
                m_time_zone = Value
            End Set
        End Property
        Private m_time_zone As String
        Public Property geo_enabled() As Boolean
            Get
                Return m_geo_enabled
            End Get
            Set(value As Boolean)
                m_geo_enabled = Value
            End Set
        End Property
        Private m_geo_enabled As Boolean
        Public Property verified() As Boolean
            Get
                Return m_verified
            End Get
            Set(value As Boolean)
                m_verified = Value
            End Set
        End Property
        Private m_verified As Boolean
        Public Property statuses_count() As Integer
            Get
                Return m_statuses_count
            End Get
            Set(value As Integer)
                m_statuses_count = Value
            End Set
        End Property
        Private m_statuses_count As Integer
        Public Property lang() As String
            Get
                Return m_lang
            End Get
            Set(value As String)
                m_lang = Value
            End Set
        End Property
        Private m_lang As String
        Public Property contributors_enabled() As Boolean
            Get
                Return m_contributors_enabled
            End Get
            Set(value As Boolean)
                m_contributors_enabled = Value
            End Set
        End Property
        Private m_contributors_enabled As Boolean
        Public Property is_translator() As Boolean
            Get
                Return m_is_translator
            End Get
            Set(value As Boolean)
                m_is_translator = Value
            End Set
        End Property
        Private m_is_translator As Boolean
        Public Property profile_background_color() As String
            Get
                Return m_profile_background_color
            End Get
            Set(value As String)
                m_profile_background_color = Value
            End Set
        End Property
        Private m_profile_background_color As String
        Public Property profile_background_image_url() As String
            Get
                Return m_profile_background_image_url
            End Get
            Set(value As String)
                m_profile_background_image_url = Value
            End Set
        End Property
        Private m_profile_background_image_url As String
        Public Property profile_background_image_url_https() As String
            Get
                Return m_profile_background_image_url_https
            End Get
            Set(value As String)
                m_profile_background_image_url_https = Value
            End Set
        End Property
        Private m_profile_background_image_url_https As String
        Public Property profile_background_tile() As Boolean
            Get
                Return m_profile_background_tile
            End Get
            Set(value As Boolean)
                m_profile_background_tile = Value
            End Set
        End Property
        Private m_profile_background_tile As Boolean
        Public Property profile_image_url() As String
            Get
                Return m_profile_image_url
            End Get
            Set(value As String)
                m_profile_image_url = Value
            End Set
        End Property
        Private m_profile_image_url As String
        Public Property profile_image_url_https() As String
            Get
                Return m_profile_image_url_https
            End Get
            Set(value As String)
                m_profile_image_url_https = Value
            End Set
        End Property
        Private m_profile_image_url_https As String
        Public Property profile_banner_url() As String
            Get
                Return m_profile_banner_url
            End Get
            Set(value As String)
                m_profile_banner_url = Value
            End Set
        End Property
        Private m_profile_banner_url As String
        Public Property profile_link_color() As String
            Get
                Return m_profile_link_color
            End Get
            Set(value As String)
                m_profile_link_color = Value
            End Set
        End Property
        Private m_profile_link_color As String
        Public Property profile_sidebar_border_color() As String
            Get
                Return m_profile_sidebar_border_color
            End Get
            Set(value As String)
                m_profile_sidebar_border_color = Value
            End Set
        End Property
        Private m_profile_sidebar_border_color As String
        Public Property profile_sidebar_fill_color() As String
            Get
                Return m_profile_sidebar_fill_color
            End Get
            Set(value As String)
                m_profile_sidebar_fill_color = Value
            End Set
        End Property
        Private m_profile_sidebar_fill_color As String
        Public Property profile_text_color() As String
            Get
                Return m_profile_text_color
            End Get
            Set(value As String)
                m_profile_text_color = Value
            End Set
        End Property
        Private m_profile_text_color As String
        Public Property profile_use_background_image() As Boolean
            Get
                Return m_profile_use_background_image
            End Get
            Set(value As Boolean)
                m_profile_use_background_image = Value
            End Set
        End Property
        Private m_profile_use_background_image As Boolean
        Public Property default_profile() As Boolean
            Get
                Return m_default_profile
            End Get
            Set(value As Boolean)
                m_default_profile = Value
            End Set
        End Property
        Private m_default_profile As Boolean
        Public Property default_profile_image() As Boolean
            Get
                Return m_default_profile_image
            End Get
            Set(value As Boolean)
                m_default_profile_image = Value
            End Set
        End Property
        Private m_default_profile_image As Boolean
        Public Property following() As Boolean
            Get
                Return m_following
            End Get
            Set(value As Boolean)
                m_following = Value
            End Set
        End Property
        Private m_following As Boolean
        Public Property follow_request_sent() As Boolean
            Get
                Return m_follow_request_sent
            End Get
            Set(value As Boolean)
                m_follow_request_sent = Value
            End Set
        End Property
        Private m_follow_request_sent As Boolean
        Public Property notifications() As Boolean
            Get
                Return m_notifications
            End Get
            Set(value As Boolean)
                m_notifications = Value
            End Set
        End Property
        Private m_notifications As Boolean
    End Class

    Public Class Large2
        Public Property w() As Integer
            Get
                Return m_w
            End Get
            Set(value As Integer)
                m_w = Value
            End Set
        End Property
        Private m_w As Integer
        Public Property h() As Integer
            Get
                Return m_h
            End Get
            Set(value As Integer)
                m_h = Value
            End Set
        End Property
        Private m_h As Integer
        Public Property resize() As String
            Get
                Return m_resize
            End Get
            Set(value As String)
                m_resize = Value
            End Set
        End Property
        Private m_resize As String
    End Class

    Public Class Thumb2
        Public Property w() As Integer
            Get
                Return m_w
            End Get
            Set(value As Integer)
                m_w = Value
            End Set
        End Property
        Private m_w As Integer
        Public Property h() As Integer
            Get
                Return m_h
            End Get
            Set(value As Integer)
                m_h = Value
            End Set
        End Property
        Private m_h As Integer
        Public Property resize() As String
            Get
                Return m_resize
            End Get
            Set(value As String)
                m_resize = Value
            End Set
        End Property
        Private m_resize As String
    End Class

    Public Class Small2
        Public Property w() As Integer
            Get
                Return m_w
            End Get
            Set(value As Integer)
                m_w = Value
            End Set
        End Property
        Private m_w As Integer
        Public Property h() As Integer
            Get
                Return m_h
            End Get
            Set(value As Integer)
                m_h = Value
            End Set
        End Property
        Private m_h As Integer
        Public Property resize() As String
            Get
                Return m_resize
            End Get
            Set(value As String)
                m_resize = Value
            End Set
        End Property
        Private m_resize As String
    End Class

    Public Class Medium4
        Public Property w() As Integer
            Get
                Return m_w
            End Get
            Set(value As Integer)
                m_w = Value
            End Set
        End Property
        Private m_w As Integer
        Public Property h() As Integer
            Get
                Return m_h
            End Get
            Set(value As Integer)
                m_h = Value
            End Set
        End Property
        Private m_h As Integer
        Public Property resize() As String
            Get
                Return m_resize
            End Get
            Set(value As String)
                m_resize = Value
            End Set
        End Property
        Private m_resize As String
    End Class

    Public Class Sizes2
        Public Property large() As Large2
            Get
                Return m_large
            End Get
            Set(value As Large2)
                m_large = Value
            End Set
        End Property
        Private m_large As Large2
        Public Property thumb() As Thumb2
            Get
                Return m_thumb
            End Get
            Set(value As Thumb2)
                m_thumb = Value
            End Set
        End Property
        Private m_thumb As Thumb2
        Public Property small() As Small2
            Get
                Return m_small
            End Get
            Set(value As Small2)
                m_small = Value
            End Set
        End Property
        Private m_small As Small2
        Public Property medium() As Medium4
            Get
                Return m_medium
            End Get
            Set(value As Medium4)
                m_medium = Value
            End Set
        End Property
        Private m_medium As Medium4
    End Class

    Public Class Medium3
        Public Property id() As Long
            Get
                Return m_id
            End Get
            Set(value As Long)
                m_id = Value
            End Set
        End Property
        Private m_id As Long
        Public Property id_str() As String
            Get
                Return m_id_str
            End Get
            Set(value As String)
                m_id_str = Value
            End Set
        End Property
        Private m_id_str As String
        Public Property indices() As List(Of Integer)
            Get
                Return m_indices
            End Get
            Set(value As List(Of Integer))
                m_indices = Value
            End Set
        End Property
        Private m_indices As List(Of Integer)
        Public Property media_url() As String
            Get
                Return m_media_url
            End Get
            Set(value As String)
                m_media_url = Value
            End Set
        End Property
        Private m_media_url As String
        Public Property media_url_https() As String
            Get
                Return m_media_url_https
            End Get
            Set(value As String)
                m_media_url_https = Value
            End Set
        End Property
        Private m_media_url_https As String
        Public Property url() As String
            Get
                Return m_url
            End Get
            Set(value As String)
                m_url = Value
            End Set
        End Property
        Private m_url As String
        Public Property display_url() As String
            Get
                Return m_display_url
            End Get
            Set(value As String)
                m_display_url = Value
            End Set
        End Property
        Private m_display_url As String
        Public Property expanded_url() As String
            Get
                Return m_expanded_url
            End Get
            Set(value As String)
                m_expanded_url = Value
            End Set
        End Property
        Private m_expanded_url As String
        Public Property type() As String
            Get
                Return m_type
            End Get
            Set(value As String)
                m_type = Value
            End Set
        End Property
        Private m_type As String
        Public Property sizes() As Sizes2
            Get
                Return m_sizes
            End Get
            Set(value As Sizes2)
                m_sizes = Value
            End Set
        End Property
        Private m_sizes As Sizes2
    End Class

    Public Class Entities4
        Public Property hashtags() As List(Of Object)
            Get
                Return m_hashtags
            End Get
            Set(value As List(Of Object))
                m_hashtags = Value
            End Set
        End Property
        Private m_hashtags As List(Of Object)
        Public Property symbols() As List(Of Object)
            Get
                Return m_symbols
            End Get
            Set(value As List(Of Object))
                m_symbols = Value
            End Set
        End Property
        Private m_symbols As List(Of Object)
        Public Property urls() As List(Of Object)
            Get
                Return m_urls
            End Get
            Set(value As List(Of Object))
                m_urls = Value
            End Set
        End Property
        Private m_urls As List(Of Object)
        Public Property user_mentions() As List(Of Object)
            Get
                Return m_user_mentions
            End Get
            Set(value As List(Of Object))
                m_user_mentions = Value
            End Set
        End Property
        Private m_user_mentions As List(Of Object)
        Public Property media() As List(Of Medium3)
            Get
                Return m_media
            End Get
            Set(value As List(Of Medium3))
                m_media = Value
            End Set
        End Property
        Private m_media As List(Of Medium3)
    End Class

    Public Class RetweetedStatus
        Public Property metadata() As Metadata2
            Get
                Return m_metadata
            End Get
            Set(value As Metadata2)
                m_metadata = Value
            End Set
        End Property
        Private m_metadata As Metadata2
        Public Property created_at() As String
            Get
                Return m_created_at
            End Get
            Set(value As String)
                m_created_at = Value
            End Set
        End Property
        Private m_created_at As String
        Public Property id() As Object
            Get
                Return m_id
            End Get
            Set(value As Object)
                m_id = Value
            End Set
        End Property
        Private m_id As Object
        Public Property id_str() As String
            Get
                Return m_id_str
            End Get
            Set(value As String)
                m_id_str = Value
            End Set
        End Property
        Private m_id_str As String
        Public Property text() As String
            Get
                Return m_text
            End Get
            Set(value As String)
                m_text = Value
            End Set
        End Property
        Private m_text As String
        Public Property source() As String
            Get
                Return m_source
            End Get
            Set(value As String)
                m_source = Value
            End Set
        End Property
        Private m_source As String
        Public Property truncated() As Boolean
            Get
                Return m_truncated
            End Get
            Set(value As Boolean)
                m_truncated = Value
            End Set
        End Property
        Private m_truncated As Boolean
        Public Property in_reply_to_status_id() As System.Nullable(Of Long)
            Get
                Return m_in_reply_to_status_id
            End Get
            Set(value As System.Nullable(Of Long))
                m_in_reply_to_status_id = Value
            End Set
        End Property
        Private m_in_reply_to_status_id As System.Nullable(Of Long)
        Public Property in_reply_to_status_id_str() As String
            Get
                Return m_in_reply_to_status_id_str
            End Get
            Set(value As String)
                m_in_reply_to_status_id_str = Value
            End Set
        End Property
        Private m_in_reply_to_status_id_str As String
        Public Property in_reply_to_user_id() As System.Nullable(Of Integer)
            Get
                Return m_in_reply_to_user_id
            End Get
            Set(value As System.Nullable(Of Integer))
                m_in_reply_to_user_id = Value
            End Set
        End Property
        Private m_in_reply_to_user_id As System.Nullable(Of Integer)
        Public Property in_reply_to_user_id_str() As String
            Get
                Return m_in_reply_to_user_id_str
            End Get
            Set(value As String)
                m_in_reply_to_user_id_str = Value
            End Set
        End Property
        Private m_in_reply_to_user_id_str As String
        Public Property in_reply_to_screen_name() As String
            Get
                Return m_in_reply_to_screen_name
            End Get
            Set(value As String)
                m_in_reply_to_screen_name = Value
            End Set
        End Property
        Private m_in_reply_to_screen_name As String
        Public Property user() As User2
            Get
                Return m_user
            End Get
            Set(value As User2)
                m_user = Value
            End Set
        End Property
        Private m_user As User2
        Public Property geo() As Object
            Get
                Return m_geo
            End Get
            Set(value As Object)
                m_geo = Value
            End Set
        End Property
        Private m_geo As Object
        Public Property coordinates() As Object
            Get
                Return m_coordinates
            End Get
            Set(value As Object)
                m_coordinates = Value
            End Set
        End Property
        Private m_coordinates As Object
        Public Property place() As Object
            Get
                Return m_place
            End Get
            Set(value As Object)
                m_place = Value
            End Set
        End Property
        Private m_place As Object
        Public Property contributors() As Object
            Get
                Return m_contributors
            End Get
            Set(value As Object)
                m_contributors = Value
            End Set
        End Property
        Private m_contributors As Object
        Public Property retweet_count() As Integer
            Get
                Return m_retweet_count
            End Get
            Set(value As Integer)
                m_retweet_count = Value
            End Set
        End Property
        Private m_retweet_count As Integer
        Public Property favorite_count() As Integer
            Get
                Return m_favorite_count
            End Get
            Set(value As Integer)
                m_favorite_count = Value
            End Set
        End Property
        Private m_favorite_count As Integer
        Public Property entities() As Entities4
            Get
                Return m_entities
            End Get
            Set(value As Entities4)
                m_entities = Value
            End Set
        End Property
        Private m_entities As Entities4
        Public Property favorited() As Boolean
            Get
                Return m_favorited
            End Get
            Set(value As Boolean)
                m_favorited = Value
            End Set
        End Property
        Private m_favorited As Boolean
        Public Property retweeted() As Boolean
            Get
                Return m_retweeted
            End Get
            Set(value As Boolean)
                m_retweeted = Value
            End Set
        End Property
        Private m_retweeted As Boolean
        Public Property possibly_sensitive() As Boolean
            Get
                Return m_possibly_sensitive
            End Get
            Set(value As Boolean)
                m_possibly_sensitive = Value
            End Set
        End Property
        Private m_possibly_sensitive As Boolean
        Public Property lang() As String
            Get
                Return m_lang
            End Get
            Set(value As String)
                m_lang = Value
            End Set
        End Property
        Private m_lang As String
    End Class

    Public Class Status
        Public Property metadata() As Metadata
            Get
                Return m_metadata
            End Get
            Set(value As Metadata)
                m_metadata = Value
            End Set
        End Property
        Private m_metadata As Metadata
        Public Property created_at() As String
            Get
                Return m_created_at
            End Get
            Set(value As String)
                m_created_at = Value
            End Set
        End Property
        Private m_created_at As String
        Public Property id() As Object
            Get
                Return m_id
            End Get
            Set(value As Object)
                m_id = Value
            End Set
        End Property
        Private m_id As Object
        Public Property id_str() As String
            Get
                Return m_id_str
            End Get
            Set(value As String)
                m_id_str = Value
            End Set
        End Property
        Private m_id_str As String
        Public Property text() As String
            Get
                Return m_text
            End Get
            Set(value As String)
                m_text = Value
            End Set
        End Property
        Private m_text As String
        Public Property source() As String
            Get
                Return m_source
            End Get
            Set(value As String)
                m_source = Value
            End Set
        End Property
        Private m_source As String
        Public Property truncated() As Boolean
            Get
                Return m_truncated
            End Get
            Set(value As Boolean)
                m_truncated = Value
            End Set
        End Property
        Private m_truncated As Boolean
        Public Property in_reply_to_status_id() As System.Nullable(Of Long)
            Get
                Return m_in_reply_to_status_id
            End Get
            Set(value As System.Nullable(Of Long))
                m_in_reply_to_status_id = Value
            End Set
        End Property
        Private m_in_reply_to_status_id As System.Nullable(Of Long)
        Public Property in_reply_to_status_id_str() As String
            Get
                Return m_in_reply_to_status_id_str
            End Get
            Set(value As String)
                m_in_reply_to_status_id_str = Value
            End Set
        End Property
        Private m_in_reply_to_status_id_str As String
        Public Property in_reply_to_user_id() As System.Nullable(Of Integer)
            Get
                Return m_in_reply_to_user_id
            End Get
            Set(value As System.Nullable(Of Integer))
                m_in_reply_to_user_id = Value
            End Set
        End Property
        Private m_in_reply_to_user_id As System.Nullable(Of Integer)
        Public Property in_reply_to_user_id_str() As String
            Get
                Return m_in_reply_to_user_id_str
            End Get
            Set(value As String)
                m_in_reply_to_user_id_str = Value
            End Set
        End Property
        Private m_in_reply_to_user_id_str As String
        Public Property in_reply_to_screen_name() As String
            Get
                Return m_in_reply_to_screen_name
            End Get
            Set(value As String)
                m_in_reply_to_screen_name = Value
            End Set
        End Property
        Private m_in_reply_to_screen_name As String
        Public Property user() As User
            Get
                Return m_user
            End Get
            Set(value As User)
                m_user = Value
            End Set
        End Property
        Private m_user As User
        Public Property geo() As Geo
            Get
                Return m_geo
            End Get
            Set(value As Geo)
                m_geo = Value
            End Set
        End Property
        Private m_geo As Geo
        Public Property coordinates() As Coordinates
            Get
                Return m_coordinates
            End Get
            Set(value As Coordinates)
                m_coordinates = Value
            End Set
        End Property
        Private m_coordinates As Coordinates
        Public Property place() As Place
            Get
                Return m_place
            End Get
            Set(value As Place)
                m_place = Value
            End Set
        End Property
        Private m_place As Place
        Public Property contributors() As Object
            Get
                Return m_contributors
            End Get
            Set(value As Object)
                m_contributors = Value
            End Set
        End Property
        Private m_contributors As Object
        Public Property retweet_count() As Integer
            Get
                Return m_retweet_count
            End Get
            Set(value As Integer)
                m_retweet_count = Value
            End Set
        End Property
        Private m_retweet_count As Integer
        Public Property favorite_count() As Integer
            Get
                Return m_favorite_count
            End Get
            Set(value As Integer)
                m_favorite_count = Value
            End Set
        End Property
        Private m_favorite_count As Integer
        Public Property entities() As Entities2
            Get
                Return m_entities
            End Get
            Set(value As Entities2)
                m_entities = Value
            End Set
        End Property
        Private m_entities As Entities2
        Public Property favorited() As Boolean
            Get
                Return m_favorited
            End Get
            Set(value As Boolean)
                m_favorited = Value
            End Set
        End Property
        Private m_favorited As Boolean
        Public Property retweeted() As Boolean
            Get
                Return m_retweeted
            End Get
            Set(value As Boolean)
                m_retweeted = Value
            End Set
        End Property
        Private m_retweeted As Boolean
        Public Property lang() As String
            Get
                Return m_lang
            End Get
            Set(value As String)
                m_lang = Value
            End Set
        End Property
        Private m_lang As String
        Public Property possibly_sensitive() As System.Nullable(Of Boolean)
            Get
                Return m_possibly_sensitive
            End Get
            Set(value As System.Nullable(Of Boolean))
                m_possibly_sensitive = Value
            End Set
        End Property
        Private m_possibly_sensitive As System.Nullable(Of Boolean)
        Public Property retweeted_status() As RetweetedStatus
            Get
                Return m_retweeted_status
            End Get
            Set(value As RetweetedStatus)
                m_retweeted_status = Value
            End Set
        End Property
        Private m_retweeted_status As RetweetedStatus
    End Class

    Public Class SearchMetadata
        Public Property completed_in() As Double
            Get
                Return m_completed_in
            End Get
            Set(value As Double)
                m_completed_in = Value
            End Set
        End Property
        Private m_completed_in As Double
        Public Property max_id() As Long
            Get
                Return m_max_id
            End Get
            Set(value As Long)
                m_max_id = Value
            End Set
        End Property
        Private m_max_id As Long
        Public Property max_id_str() As String
            Get
                Return m_max_id_str
            End Get
            Set(value As String)
                m_max_id_str = Value
            End Set
        End Property
        Private m_max_id_str As String
        Public Property next_results() As String
            Get
                Return m_next_results
            End Get
            Set(value As String)
                m_next_results = Value
            End Set
        End Property
        Private m_next_results As String
        Public Property query() As String
            Get
                Return m_query
            End Get
            Set(value As String)
                m_query = Value
            End Set
        End Property
        Private m_query As String
        Public Property refresh_url() As String
            Get
                Return m_refresh_url
            End Get
            Set(value As String)
                m_refresh_url = Value
            End Set
        End Property
        Private m_refresh_url As String
        Public Property count() As Integer
            Get
                Return m_count
            End Get
            Set(value As Integer)
                m_count = Value
            End Set
        End Property
        Private m_count As Integer
        Public Property since_id() As Integer
            Get
                Return m_since_id
            End Get
            Set(value As Integer)
                m_since_id = Value
            End Set
        End Property
        Private m_since_id As Integer
        Public Property since_id_str() As String
            Get
                Return m_since_id_str
            End Get
            Set(value As String)
                m_since_id_str = Value
            End Set
        End Property
        Private m_since_id_str As String
    End Class

    Public Class RootObject
        Public Property statuses() As List(Of Status)
            Get
                Return m_statuses
            End Get
            Set(value As List(Of Status))
                m_statuses = Value
            End Set
        End Property
        Private m_statuses As List(Of Status)
        Public Property search_metadata() As SearchMetadata
            Get
                Return m_search_metadata
            End Get
            Set(value As SearchMetadata)
                m_search_metadata = Value
            End Set
        End Property
        Private m_search_metadata As SearchMetadata
    End Class
End Class
