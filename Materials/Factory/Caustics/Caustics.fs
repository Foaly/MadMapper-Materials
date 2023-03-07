/*{
    "CREDIT": "Mad Team",
    "DESCRIPTION": "Caustics noise.",
    "VSN": "1.0",
    "TAGS": "noise",
    "INPUTS": [
        { "LABEL": "Speed", "NAME": "speed", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 0.5 },
        { "LABEL": "Reverse", "NAME": "reverse", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
        { "LABEL": "Scale", "NAME": "scale", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 4.0 },
        { "LABEL": "Brightness", "NAME": "brightness", "TYPE": "float", "MIN": -0.5, "MAX": 0.5, "DEFAULT": 0.0 },
        { "LABEL": "Contrast", "NAME": "contrast", "TYPE": "float", "DEFAULT": 2.0, "MIN": 1.0, "MAX": 3.0 }
    ],
    "GENERATORS": [
        { "NAME": "animation_time", "TYPE": "time_base", "PARAMS": {"speed": "speed", "reverse": "reverse", "link_speed_to_global_bpm":true} }
    ]
}*/

#define MAX_ITER 3

vec4 materialColorForPixel(vec2 texCoord)
{
    vec2 uv = vec2(0.5,0.5) + (texCoord-vec2(0.5,0.5)) * scale;
    vec2 p =  uv * 8.0 - vec2(20.0);
    vec2 i = p;
    float c = 1.0;
    float inten = .05;

    for (int n = 0; n < MAX_ITER; n++)
    {
        float t = animation_time * (1.0 - (3.0 / float(n+1)));

        i = p + vec2(cos(t - i.x) + sin(t + i.y),
        sin(t - i.y) + cos(t + i.x));
    
        c += 1.0/length(vec2(p.x / (sin(i.x+t)/inten),
        p.y / (cos(i.y+t)/inten)));
    }

    c /= float(MAX_ITER);
    c = 1.5 - sqrt(c);

    float value = 0.1 / (1.0 - (c + 0.05));
    value += brightness;
    value = mix(0.5, value, contrast);

    return vec4(vec3(value),1);
}
