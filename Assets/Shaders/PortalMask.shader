Shader "Custom/PortalMask" {
    SubShader {
        // Render before everything else
        Tags { "RenderType"="Opaque" "Queue"="Geometry-1" }
        
        // Don't draw any color or write to depth
        ColorMask 0
        ZWrite Off

        Pass {
            Stencil {
                Ref 1
                Comp Always
                Pass Replace
            }
        }
    }
}