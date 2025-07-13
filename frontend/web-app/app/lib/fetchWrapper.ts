import { auth } from "@/auth";

const basUrl = 'http://localhost:6001/';


async function get(url : string){
    const requestOptions = {
        method: 'GET',
        headers: await getHeaders(),
    }

    const response = await fetch(basUrl + url, requestOptions);
    return handleResponse(response);
}
 

async function put(url : string, body: unknown){
    const requestOptions = {
        method: 'PUT',
        headers: await getHeaders(),
        body: JSON.stringify(body)
    }

    const response = await fetch(basUrl + url, requestOptions);
    return handleResponse(response);
}

async function post(url : string, body: unknown){
    const requestOptions = {
        method: 'POST',
        headers: await getHeaders(),
        body: JSON.stringify(body)
    }

    const response = await fetch(basUrl + url, requestOptions);
    return handleResponse(response);
}

async function del(url : string){
    const requestOptions = {
        method: 'DELETE',
        headers: await getHeaders()
    }

    const response = await fetch(basUrl + url, requestOptions);
    return handleResponse(response);
}

async function getHeaders(): Promise<Headers> {
    const session = await auth();
    const headers = new Headers();
    headers.set('Content-Type', 'application/json');
    if (session) {
        // console.log('Session accessToken:', session.accessToken);
        headers.set('Authorization', `Bearer ${session.accessToken}`);
    }
    return headers;
}

async function handleResponse(response: Response) {
    const text = await response.text();
    console.log('Response text:', text);
    const data = text && JSON.parse(text);
     console.log('Parsed data:', data);
    if (response.ok) {
        return data || response
    }else{
        const error = {
            status: response.status,
            message: response.statusText
        }
        return error;
    }
}

export const fetchWrapper = {
    get,
    put,
    post,
    del
};