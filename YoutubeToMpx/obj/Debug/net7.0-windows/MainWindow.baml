   M S B A M L   `   `   `  ����G  CYoutubeToMpx, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:YoutubeDownloader.ControlsYoutubeDownloader.Controls  5   	   G(clr-namespace:YoutubeDownloader.ControlsYoutubeDownloader.Controls  5   	   "    YoutubeDownloader.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:YoutubeDownloader.Controls     5   	   =controls(clr-namespace:YoutubeDownloader.Controls     5   	     � Title$  Youtube to Mpx��5	   	   $	��720��6    $
��1280��6-    � 
ResizeMode$ CanMinimize=�5
   	   .��5      � ���#333333 � RowDefinitions 5   
   �� 5       � ColumnDefinitions 5   
   �� 5      $	��125 �6    �� 5      .+�5   
   �� -   5         ��Name$ ContentControl��5   )   $��1��6   6)   � 5   
   $��1��6   $��0��6   ���#121212 5      �� 5      $	��93* �6#   �� 5      $��7* �6#    5       �� 5!      -   5         �Name$ LogoRow��5!       $	��150 �6/   �� 5"      -   5        $ URLRow��5"       �� 5#      -   5        $ ConfirmationRow��5#       �� 5$      -   5        $ DownloadingRow��5$       �� 5%      -   5        $ DoneRow��5%       .+�5(      �  5)      �� 5*      $��1* �6$   �� 5+      $��2* �6$   .+�5.      �� -   5        $��LogoSlot��5.      $��2��6(   50      �� 55      -   5        $��URLRect��55   B   $��1��6   ���#121212 J� Opacity$	 0.5Q�64   $��2��6Q   �� 56      $��1��6   $��Dosis)�6%   $	��URL��68   
�������$
��Left=�6V   $��Center=�6q   $��34,0,0,0q�6�   $��20(�6�   �� 5;      -	   5        $��ConfirmationRect��5;   C   $��1��6   ���#121212$	 0.5Q�65   $��2��6[   $��	0,117,0,0q�6o   $��2��6�   �� 5<      $��2��6   $��Dosis)�6%   $��Confirmation��68   
�������$
��Left=�6_   $��Center=�6z   $��10,0,0,0q�6�   $��16(�6�   �� 5A      -
   5        $��DownloadingRect��5A   C   $��2��6   ���#121212$	 0.5Q�65   $��2��6Z   $��0,117,0,117q�6n   $��3��6�   �� 5B      $��3��6   $��Dosis)�6%   $��Downloading��68   
�������$
��Left=�6^   $��Center=�6y   $��11,0,0,0q�6�   $��16(�6�   �� 5G      -   5        $��DoneRect��5G   C   $��4��6   ���#121212$	 0.5Q�65   $��2��6S   �� 5H      $��4��6   $��Dosis)�6%   $
��Done��68   
�������$
��Left=�6W   $��Center=�6r   $��30,0,0,0q�6�   $��20(�6�   5J      5M      5N      