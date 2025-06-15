'use server';
import { auth } from "@/auth";
import { Auction, PagedResult } from "@/index";
import { Console } from "console";


export async function getData(query: string): Promise<PagedResult<Auction>> {
    const res = await fetch(`http://localhost:6001/search${query}`);
    if (!res.ok) { 
        throw new Error('Failed to fetch data');
    }
    return res.json();
}


export async function updateAuctionTest(): Promise<{status: number, message: string}> {
    const session = await auth();
    const data = {
        mileage: Math.floor(Math.random() * 10000) + 1
    } 
    console.log('data:', data);
    const res = await fetch('http://localhost:6001/auctions/afbee524-5972-4075-8800-7d1f9d7b0a0c', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${session?.accessToken}`
        },
        body: JSON.stringify(data)
    });
    console.log('JSON.stringify(data):', JSON.stringify(data));

    if (!res.ok) return {status: res.status, message: await res.statusText}

    return  {status: res.status, message: await res.statusText};
}