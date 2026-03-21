Shader "Custom/PortalMask" {
    SubShader {
        // Render before everything else
        Tags { "RenderType"="Opaque" "Queue"="Geometry-1" }
        
        // It is invisible, you won't see the "door" itself.
        ColorMask 0
        ZWrite Off
        Cull off // when you enter through portal and turn back then you see normal world through the doorway

        Stencil {
                Ref 1
                Comp Always
                Pass Replace
            }

        Pass {
            
        }
    }
}